# FarmeORT: ejemplo de deploy (DA2)

Una API en .NET 10 y un front en Angular 22, armados para practicar el deploy en Diseño de Aplicaciones 2 (Universidad ORT).

**La guía paso a paso está en [da2ort.com → Tecnología → Deploy](https://da2ort.com/tecnologia/deploy).** Este repo es el proyecto que usa la guía: lo podés clonar, levantar y publicar siguiendo cada paso. Para tu proyecto, cambiá `FarmeORT` por el nombre de tu solución.

## Qué hay

```text
farmeort-deploy/
├── backend/
│   ├── FarmeORT.slnx
│   ├── src/                       ← la API en capas (Dominio, Servicios, Repositorios, WebApi…)
│   └── Datos/
│       ├── docker-compose.yml     ← SQL Server en Docker
│       ├── .env.example           ← copialo a .env y poné una contraseña
│       └── FarmeORT-esquema-y-datos.sql
└── frontend/                      ← el front Angular
    ├── public/config.json         ← la URL de la API
    └── wrangler.jsonc             ← la publicación en Cloudflare
```

- La API lee el connection string y los orígenes de CORS de la configuración (variables de entorno en lo publicado).
- Al arrancar aplica las migraciones y carga datos de prueba.
- `GET /api/salud` dice si la API anda y si llega a la base.
- El front lee la URL de la API de `config.json` al arrancar, así que se puede cambiar sin recompilar.

## Requisitos

- .NET SDK 10
- Node 22.22 o superior (en Mac, `brew install node` o el instalador de nodejs.org; no `node@22`)
- Docker
- cloudflared (solo para publicar en internet)

## Levantarlo en tu máquina

```bash
# 1. Base de datos (desde backend/Datos)
cd backend/Datos
cp .env.example .env            # y cambiá la contraseña (funciona igual en PowerShell)
docker compose up -d

# 2. Publicar la API (desde backend)
cd ..
dotnet publish src/FarmeORT.WebApi -c Release -o Aplicacion

# 3. Correr lo publicado (Mac o Linux)
cd Aplicacion
export ConnectionStrings__FarmeORT='Server=localhost,1433;Database=FarmeORT;User Id=sa;Password=la-del-.env;Encrypt=False;TrustServerCertificate=True'
export Cors__OrigenesPermitidos__0='http://localhost:8080'
dotnet FarmeORT.WebApi.dll --urls http://localhost:5080
```

Cambiá `la-del-.env` por tu contraseña. Las comillas van simples: con dobles, la terminal cambia los `$` y `!` de la contraseña.

En Windows (PowerShell), el paso 3 es:

```powershell
cd Aplicacion
$env:ConnectionStrings__FarmeORT = 'Server=localhost,1433;Database=FarmeORT;User Id=sa;Password=la-del-.env;Encrypt=False;TrustServerCertificate=True'
$env:Cors__OrigenesPermitidos__0 = 'http://localhost:8080'
dotnet FarmeORT.WebApi.dll --urls http://localhost:5080
```

Probá `http://localhost:5080/api/salud`: tiene que decir `"estado": "OK"` y `"baseDeDatosConectada": true`.

```bash
# 4. Front (desde frontend, en otra terminal)
npm ci
npm run build                  # deja el sitio en dist/frontend/browser
npm run servir:dist            # lo sirve en http://localhost:8080
```

Abrí `http://localhost:8080`: la barra de arriba tiene que estar en verde.

Para publicarlo en internet con Cloudflare (túnel para la API y Workers para el front), seguí la sección 6 de la guía.

## Extra: importadores en caliente

La API también carga importadores de skills desde la carpeta `Plugins`, sin reiniciar. Con la API corriendo:

```bash
cd backend
dotnet build src/FarmeORT.Importadores.Xml -c Release
cp src/FarmeORT.Importadores.Xml/bin/Release/net10.0/FarmeORT.Importadores.Xml.dll Aplicacion/Plugins/
```

En la pantalla Importar, apretá "Actualizar lista": aparece XML.
