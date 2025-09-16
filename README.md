# GetechnologiesMx - Prueba Técnica

Este repositorio contiene el código de la prueba técnica para el puesto de Desarrollador de Software. La solución está dividida en dos proyectos, uno para el backend y otro para el frontend.

### Estructura de Proyectos

* `backend/Facturacion.API`: Proyecto **backend** desarrollado con .NET 6 que expone un servicio REST para la gestión de personas y facturas. Utiliza el patrón de diseño Repository.
* `frontend/Facturacion.WPF.Client`: Proyecto **frontend** desarrollado con WPF para Windows. Es un cliente de escritorio que consume la API del backend para el registro de usuarios.

### Tecnologías utilizadas

* **Backend**: C#, ASP.NET Core Web API, .NET 6, Entity Framework, Repository Pattern.
* **Frontend**: C#, WPF, .NET 6.

### Pasos para ejecutar la aplicación

1.  Abre la solución completa `GetechnologiesMx.sln` en Visual Studio (ubicada en la raíz del repo).
2.  Ejecuta el proyecto `backend/Facturacion.API` para iniciar el servidor de la API.
3.  Ejecuta el proyecto `frontend/Facturacion.WPF.Client` para iniciar la aplicación de escritorio.

---

## Notas sobre la reorganización (acciones realizadas)

Al preparar este repo para que contenga ambos proyectos en una única estructura (monorepo) realicé las siguientes acciones:

- Creé una rama nueva llamada `monorepo-structure` y trabajé sobre ella.
- Añadí carpetas de nivel raíz `backend/` y `frontend/` y copié los proyectos:
  - `Facturacion.API` → `backend/Facturacion.API`
  - `Facturacion.WPF.Client` → `frontend/Facturacion.WPF.Client`
- Creé una solución raíz `GetechnologiesMx.sln` y añadí ambos proyectos para facilitar la apertura y el build desde Visual Studio.
- Añadí un `.gitignore` orientado a Visual Studio/.NET y limpié los artefactos más notorios (`bin/`, `obj/`) antes del commit cuando fue posible.
- Hice commit y push de los cambios a la rama `monorepo-structure` en el remoto `origin`.

Si revisas el historial de commits en GitHub verás el commit con el mensaje: "monorepo: agregar backend y frontend en estructura monorepo".

### Notas y recomendaciones

- Algunos archivos bin/obj del frontend (WPF) pueden haberse incluido accidentalmente si ya estaban presentes antes de añadir el `.gitignore`. Recomiendo revisar y limpiar esos artefactos y, si procede, hacer un nuevo commit que los elimine del repo.
- Si prefieres que en lugar de copiar los proyectos los mueva (eliminar las copias locales), puedo ayudarte a hacerlo y a sincronizar el estado local con el remoto.
- Si quieres, abro el Pull Request en GitHub o preparo la rama para revisión y merge.

Si necesitas que haga alguno de los pasos siguientes (limpiar bin/obj, abrir PR, mover en lugar de copiar, ajustar `launchSettings` o crear scripts de build), dime cuál y lo hago.
# GetechnologiesMx