using OficiosTI.Aplicacion.DTOs;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.Services;
using OficiosTI.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OficiosTI
{
    public partial class FrmPanelTickets : Form
    {
        private readonly OficiosContext _context;
        private OficioRespuestaService _service;
        public FrmPanelTickets(OficiosContext context)
        {
            InitializeComponent();
            _context = context;
            _service = new OficioRespuestaService(_context);
            tabControlMain.SelectedIndexChanged += tabControlMain_SelectedIndexChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SigelicXPTheme.ApplyForm(this);
            SigelicXPTheme.ApplyStartButton(btnAdjuntos);
            CargarPendientes();

        }
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControlMain.SelectedIndex)
            {
                case 0:
                    CargarPendientes();
                    break;

                case 1:
                    CargarCerradosSinOficio();
                    break;

                case 2:
                    CargarPorAnalista();
                    break;

            //  case 3:
            //     CargarTodos();
            //     break;
            }
        }

        private void BtnAdjuntos_Click(object sender, EventArgs e)
        {
            var ticket = ObtenerTicketSeleccionado();
            if (ticket == null)
                return;
            var frm = new FrmAdjuntos(ticket.TicketId, _context);
            frm.ShowDialog();
        }

        private void BtnAbrirTicket_Click(object sender, EventArgs e)
        {
            var ticket = ObtenerTicketSeleccionado();

            if (ticket == null)
                return;

            new FrmTicketDetalle(ticket, _context).ShowDialog();
        }

        private void BtnGenerarOficio_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridTickets.CurrentRow == null)
                    return;

                var gridItem = (TicketGridModel)DataGridTickets.CurrentRow.DataBoundItem;

                if (gridItem == null)
                    return;

                if (!string.IsNullOrEmpty(gridItem.NumeroOficio))
                {
                    var r = MessageBox.Show(
                        "Este ticket ya tiene oficio. ¿Desea editarlo?",
                        "Oficio existente",
                        MessageBoxButtons.YesNo);

                    if (r == DialogResult.No)
                        return;
                }
                var ticket = ObtenerTicketSeleccionado();
                if (ticket == null)
                    return;
                new FrmOficioRespuesta(ticket, _context).ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private Ticket ObtenerTicketSeleccionado()
        {
            if (DataGridTickets.CurrentRow == null)
                return null;

            var gridItem = (TicketGridModel)DataGridTickets.CurrentRow.DataBoundItem;

            return _context.Ticket.Find(gridItem.TicketId);
        }

        private void ActualizarIndicadores(List<TicketGridModel> tickets)
        {
            int total = tickets.Count;

            int respondidos = tickets.Count(t =>
                !string.IsNullOrEmpty(t.NumeroOficio));

            int pendientes = total - respondidos;

            LabelTotalTickets.Text = $"Tickets derivados: {total}";
            LabelRespondidos.Text = $"Respondidos: {respondidos}";
            LabelPendientes.Text = $"Pendientes: {pendientes}";
        }


        private List<TicketGridModel> Mapear(IQueryable<Ticket> query)
        {
            return query
                .Select(t => new TicketGridModel
                {
                    TicketId = t.TicketId,
                    TicketPersona = t.TicketPersona,
                    TicketAsunto = t.TicketAsunto,
                    TicketPrioridad = t.TicketPrioridad,
                    TicketFecha = t.TicketFecha,
                    NumeroOficio = _context.OficioRespuesta
                        .Where(o => o.TicketId == t.TicketId)
                        .Select(o => o.NumeroOficio)
                        .FirstOrDefault()
                })
                .OrderByDescending(t => t.TicketFecha)
                .ToList();
        }

        private void CargarPendientes()
        {
            int miOficinaId = _service.ObtenerUnidadOrgId(_service.ObtenerUnidadOrganizativa());
            DateTime fechaLimite = DateTime.Now.AddMonths(-2);
            var query = _context.Ticket.AsQueryable();
             if (_service.EsUsuarioGlobal())
            {
                query = query.Where(t => t.id_of != null && t.Cat_TicketStatusId >= 1);
            }
            else
            {
                query = query.Where(t => t.OficinasId == miOficinaId && t.id_of != null && t.Cat_TicketStatusId >= 1);
            }
            var tickets = query
                .Select(t => new TicketGridModel
                {
                    TicketId = t.TicketId,
                    TicketPersona = t.TicketPersona,
                    TicketAsunto = t.TicketAsunto,
                    TicketPrioridad = t.TicketPrioridad,
                    TicketFecha = t.TicketFecha,
                    Cat_TicketStatusId = t.Cat_TicketStatusId,
                    EstadoTicket = _context.Cat_TicketStatus
                        .Where(o => o.Cat_TicketStatusId == t.Cat_TicketStatusId)
                        .Select(o => o.Cat_TicketStatusStatus)
                        .FirstOrDefault(),
                    OficinasNombre = _context.Oficinas
                        .Where(o => o.OficinasId == t.OficinasId)
                        .Select(o => o.OficinasNombre)
                        .FirstOrDefault(),
                    NumeroOficio = _context.OficioRespuesta
                        .Where(o => o.TicketId == t.TicketId)
                        .Select(o => o.NumeroOficio)
                        .FirstOrDefault()
                })
                .Where(t => t.TicketFecha >= fechaLimite)
                .OrderBy(t => string.IsNullOrEmpty(t.NumeroOficio) ? 1 :
                              (t.Cat_TicketStatusId == 2) ? 2 :
                              (t.Cat_TicketStatusId == 3) ? 3 :
                              4)
                .ThenByDescending(t => t.Cat_TicketStatusId)
                .ToList();
            gridPendientes.DataSource = tickets;
            ActualizarIndicadores(tickets);
        }

      private void CargarPorAnalista()
      {
            var data = BaseQuery()
            .Where(t => !_context.OficioRespuesta.Any(o => o.TicketId == t.TicketId))
            .GroupBy(t => new { t.TicketUUsuario })
            .Select(g => new TicketPorAnalistaDto
            {
                UsuarioId = g.Key.TicketUUsuario,
                //Analista = g.Key.UsuarioNombre,
                Total = g.Count(),
                Urgentes = g.Count(t => t.TicketPrioridad == "Alta")
            })
            .OrderByDescending(x => x.Urgentes)
            .ThenByDescending(x => x.Total)
            .ToList();
            gridAnalista.DataSource = data;
      }
   
/*    private void CargarPorAnalista()
      {
            var data = BaseQuery()
                .Where(t => !_context.OficioRespuesta.Any(o => o.TicketId == t.TicketId))
                .GroupBy(t => t.TicketUUsuario)
                .Select(g => new TicketPorAnalistaDto
                {
                    UsuarioId = g.Key, 
                    Total = g.Count(),
                    Urgentes = g.Count(t => t.TicketPrioridad == "Alta")
                })
                .OrderByDescending(x => x.Urgentes)
                .ThenByDescending(x => x.Total)
                .ToList();

            gridAnalista.DataSource = data;
       }

  */
        private void CargarCerradosSinOficio()
        {
            int miOficinaId = _service.ObtenerUnidadOrgId(_service.ObtenerUnidadOrganizativa());
            DateTime fechaLimite = DateTime.Now.AddMonths(-2);
            var query = _context.Ticket.AsQueryable();
            if (_service.EsUsuarioGlobal())
            {
                query = query.Where(t => t.id_of != null && t.Cat_TicketStatusId == 3);
            }
            else
            {
                query = query.Where(t => t.OficinasId == miOficinaId && t.id_of != null && t.Cat_TicketStatusId == 3);
            }

            var tickets = query
                .Select(t => new TicketGridModel
                {
                    TicketId = t.TicketId,
                    TicketPersona = t.TicketPersona,
                    TicketAsunto = t.TicketAsunto,
                    TicketPrioridad = t.TicketPrioridad,
                    TicketFecha = t.TicketFecha,
                    Cat_TicketStatusId = t.Cat_TicketStatusId,
                    EstadoTicket = _context.Cat_TicketStatus
                        .Where(o => o.Cat_TicketStatusId == t.Cat_TicketStatusId)
                        .Select(o => o.Cat_TicketStatusStatus)
                        .FirstOrDefault(),

                    OficinasNombre = _context.Oficinas
                        .Where(o => o.OficinasId == t.OficinasId)
                        .Select(o => o.OficinasNombre)
                        .FirstOrDefault(),

                    NumeroOficio = _context.OficioRespuesta
                        .Where(o => o.TicketId == t.TicketId)
                        .Select(o => o.NumeroOficio)
                        .FirstOrDefault()
                })
                .Where(t => t.TicketFecha >= fechaLimite)
                 .OrderBy(t => string.IsNullOrEmpty(t.NumeroOficio) ? 0 : 1)
                 .ThenByDescending(t => t.TicketFecha)
                .ToList();

            gridCerrados.DataSource = null;
            var bs = new BindingSource { DataSource = tickets };
            gridCerrados.DataSource = bs;
        }

        private IQueryable<Ticket> BaseQuery()
        {
            int miOficinaId = _service.ObtenerUnidadOrgId(_service.ObtenerUnidadOrganizativa());   /// POR DOMINIO USUARIO
            string org = _service.ObtenerUnidadOrganizativa();
            int idorg = _service.ObtenerUnidadOrgId(org);
            var query = _context.Ticket.AsQueryable();
            if (_service.EsUsuarioGlobal())
            {
              return  query = query.Where(t => t.id_of != null && t.Cat_TicketStatusId == 3);
            }
            else
            {
                return query = query.Where(t => t.OficinasId == miOficinaId && t.id_of != null && t.Cat_TicketStatusId == 3);
            }

        /*    return _context.Ticket
                .Where(t => t.OficinasId == 3);    //// 1 REDES, 2 CONTROL Y RESGUARDO, DESARRROLLO
        */

        }
        /*
             private void CargarTodos()
             {
                 var query = BaseQuery();

                 gridTodos.DataSource = Mapear(query);
             }

             private IQueryable<TicketGridModel> QueryTicketsBase()
             {
                 var fechaLimite = DateTime.Now.AddMonths(-8);

                 return
                     from t in BaseQuery()
                     join o in _context.OficioRespuesta
                         on t.TicketId equals o.TicketId into oficios
                     from o in oficios.DefaultIfEmpty()
                     where t.TicketFecha >= fechaLimite
                     select new TicketGridModel
                     {
                         TicketId = t.TicketId,
                         TicketPersona = t.TicketPersona,
                         TicketAsunto = t.TicketAsunto,
                         TicketPrioridad = t.TicketPrioridad,
                         TicketFecha = t.TicketFecha,
                         Cat_TicketStatusId = t.Cat_TicketStatusId,
                         NumeroOficio = o.NumeroOficio
                     };
             }
         */

    }
}
