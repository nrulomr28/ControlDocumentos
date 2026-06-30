# OficiosTI

> Sistema de Gestión de Oficios y Documentación de Tecnologías de la Información.

## Descripción

**OficiosTI** es una plataforma desarrollada para administrar el ciclo de vida de la correspondencia oficial del área de Tecnologías de la Información.

El sistema permite el registro, seguimiento y consulta de oficios, así como la administración de tickets relacionados, adjuntos y futuras funcionalidades orientadas a la gestión documental.

## Objetivos

* Centralizar el control de oficios.
* Dar seguimiento a solicitudes mediante tickets.
* Gestionar documentos adjuntos.
* Mantener trazabilidad de las operaciones.
* Facilitar la evolución del sistema mediante una arquitectura modular.

## Arquitectura

El proyecto está organizado en las siguientes capas:

* **Presentación (WinForms)**
* **Aplicación**
* **Dominio**
* **Infraestructura**
* **Compartido**

Esta estructura permite mantener una separación clara entre la interfaz de usuario, la lógica de negocio y el acceso a datos.

## Módulos actuales

* Gestión de Oficios
* Gestión de Tickets
* Adjuntos

## Tecnologías

* .NET
* Windows Forms
* Entity Framework Core
* SQL Server
* QuestPDF

## Flujo de ramas

* `main` → versión estable.
* `develop` → integración y desarrollo.
* `feature/*` → nuevas funcionalidades.
* `hotfix/*` → correcciones urgentes.
* `release/*` → preparación de versiones.

## Estado del proyecto

Versión base de la arquitectura oficial.

**Foundation:** `v0.1.0-foundation`
