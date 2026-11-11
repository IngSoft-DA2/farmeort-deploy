import { Routes } from '@angular/router';
import { SkillsPagina } from './skills/skills-pagina';
import { ImportarPagina } from './importar/importar-pagina';

// Rutas "profundas" (/skills, /importar): sirven para probar que el hosting devuelve index.html
// al recargar con F5. Si el servidor no está configurado para una SPA, da 404.
export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'skills' },
  { path: 'skills', component: SkillsPagina, title: 'Skills · FarmeORT' },
  { path: 'importar', component: ImportarPagina, title: 'Importar · FarmeORT' },
  { path: '**', redirectTo: 'skills' },
];
