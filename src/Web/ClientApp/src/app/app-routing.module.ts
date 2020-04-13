import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotesComponent } from './notes/notes.component';
import { NoteComponent } from './note/note.component';
import { NoteAddEditComponent } from './note-add-edit/note-add-edit.component';


const routes: Routes = [
  { path:'', component: NotesComponent, pathMatch:'full'},
  { path: 'note/:id', component: NoteComponent },
  { path: 'add', component: NoteAddEditComponent },
  { path: 'note/edit/:id', component: NoteAddEditComponent },
  { path: '**', redirectTo: '/'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
