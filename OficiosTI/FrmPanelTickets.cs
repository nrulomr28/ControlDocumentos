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
            this.Font = new Font("Gotham", 10F, FontStyle.Regular, GraphicsUnit.Point);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SigelicXPTheme.ApplyForm(this);
            SigelicXPTheme.ApplyStartButton(btnAdjuntos);
            CargarPendientes();
            tabControlMain.TabPages.Remove(tabPendientes);    // ocultar los tab 
            tabControlMain.TabPages.Remove(tabCerrados);      // ocultar los tab
            tabControlMain.TabPages.Remove(tabTodos);         // ocultar los tab

        }
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Habilitamos los botones por defecto
            bool permitirAcciones = true;
            switch (tabControlMain.SelectedIndex)
            {
                case 0:
                    CargarPorAnalista();
                    permitirAcciones = false;
                    break;
         /*     case 1:
                    CargarCerradosSinOficio();
                    break;
                case 2:
                    CargarPendientes();
                    break;
                case 3:
                    CargarTodos();
                    break;
        */
            }
            btnAdjuntos.Enabled = permitirAcciones;
            btnAbrir.Enabled    = permitirAcciones; 
            btnGenerar.Enabled  = permitirAcciones; 
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
                 if (gridPendientes.CurrentRow == null)
                   return;

                var gridItem = (TicketGridModel)gridPendientes.CurrentRow.DataBoundItem;
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
            DataGridView gridActivo = null;           
            switch (tabControlMain.SelectedIndex)
            {
                case 0:
                    gridActivo = gridPendientes;
                    break;
                case 1:
                    gridActivo = gridCerrados;
                    break;
                case 2:                   
                    gridActivo = gridAnalista;
                    break;
                case 3:
                    gridActivo = gridTodos;
                    break;
            }          
            if (gridActivo == null || gridActivo.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecciona un ticket de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }         
            var gridItem = gridActivo.CurrentRow.DataBoundItem as TicketGridModel;           
            if (gridItem == null)
            {
                MessageBox.Show("No se puede abrir un ticket desde esta vista. Selecciona un ticket individual.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }          
            return _context.Ticket.Find(gridItem.TicketId);
        }

        private void ActualizarIndicadores(List<TicketGridModel> tickets)
        {
            int total = tickets.Count;
            int respondidos = tickets.Count(t =>
                !string.IsNullOrEmpty(t.NumeroOficio));
            int pendientes = total - respondidos;
            LabelTotalTickets.Text = $"Tickets derivados: {total}";
            LabelRespondidos.Text  = $"Respondidos: {respondidos}";
            LabelPendientes.Text   = $"Pendientes: {pendientes}";
        }


     /* private List<TicketGridModel> Mapear(IQueryable<Ticket> query)
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
     */
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
            var datosPlanos = BaseQuery()              
                .Join(
                    _context.Usuarios,
                    t => t.TicketUUsuario,
                    u => u.UsuarioAnterior,
                    (t, u) => new { Ticket = t, Usuario = u } 
                )              
                .Join(
                    _context.Oficinas,
                    tu => tu.Ticket.OficinasId,        
                    ofi => ofi.OficinasId,             
                    (tu, ofi) => new
                    {
                        UsuarioAnterior = tu.Ticket.TicketUUsuario,
                        AnalistaNombre = tu.Usuario.NombreCompleto,
                        Prioridad = tu.Ticket.TicketPrioridad,
                        Status = tu.Ticket.Cat_TicketStatusId,                     
                        OficinaNombre = ofi.OficinasNombre
                    }
                )
                .ToList();            
            var data = datosPlanos               
                .GroupBy(x => new { x.UsuarioAnterior, x.AnalistaNombre, x.OficinaNombre })
                .Select(g => new TicketPorAnalistaDto
                {
                    UsuarioId = g.Key.UsuarioAnterior,
                    Analista = g.Key.AnalistaNombre,                
                    Oficina = g.Key.OficinaNombre,
                    Total = g.Count(),
                    Urgentes = g.Count(x => x.Prioridad == "URGENTE"),
                    Terminados = g.Count(x => x.Status == 3)
                })
                .OrderByDescending(x => x.Urgentes)
                .ThenByDescending(x => x.Total)
                .ToList();
            gridAnalista.DataSource = data;
        }


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
            int miOficinaId = _service.ObtenerUnidadOrgId(_service.ObtenerUnidadOrganizativa());
            var query = _context.Ticket.AsQueryable();

            if (_service.EsUsuarioGlobal())
            {
             
                return query.Where(t => t.id_of != null && t.Cat_TicketStatusId >= 1);
            }
            else
            {

                return query.Where(t => t.OficinasId == miOficinaId && t.id_of != null && t.Cat_TicketStatusId >= 1);
            }
        }

        private void CargarTodos()
        {           
            var tickets = QueryTicketsBase()
                .OrderByDescending(t => t.TicketFecha)
                .ToList();
            gridTodos.DataSource = tickets;
        }

        private IQueryable<TicketGridModel> QueryTicketsBase()
        {
            var fechaLimite = DateTime.Now.AddMonths(-2);

            return
                from t in BaseQuery()                
                join o in _context.OficioRespuesta
                    on t.TicketId equals o.TicketId into oficios
                from o in oficios.DefaultIfEmpty()
                join s in _context.Cat_TicketStatus
                    on t.Cat_TicketStatusId equals s.Cat_TicketStatusId into estados
                from s in estados.DefaultIfEmpty()
                join ofi in _context.Oficinas
                    on t.OficinasId equals ofi.OficinasId into oficinas
                from ofi in oficinas.DefaultIfEmpty()
                where t.TicketFecha >= fechaLimite
                select new TicketGridModel
                {
                    TicketId = t.TicketId,
                    TicketPersona = t.TicketPersona,
                    TicketAsunto = t.TicketAsunto,
                    TicketPrioridad = t.TicketPrioridad,
                    TicketFecha = t.TicketFecha,
                    Cat_TicketStatusId = t.Cat_TicketStatusId,                
                    NumeroOficio = o != null ? o.NumeroOficio : null,
                    EstadoTicket = s != null ? s.Cat_TicketStatusStatus : null,
                    OficinasNombre = ofi != null ? ofi.OficinasNombre : null
                };
        }


    }
}

