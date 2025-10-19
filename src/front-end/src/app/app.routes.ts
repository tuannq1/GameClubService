import { Routes } from '@angular/router';
import { ClubList } from './pages/clubs/club-list/club-list';
import { ClubForm} from './pages/clubs/club-form/club-form';
import { EventList } from './pages/events/event-list/event-list';
import { EventForm } from './pages/events/event-form/event-form';

export const routes: Routes = [
  { path: '', redirectTo: 'clubs', pathMatch: 'full' },
  { path: 'clubs', component: ClubList },
  { path: 'clubs/create', component: ClubForm },
  { path: 'events', component: EventList },
  { path: 'events/create', component: EventForm },
];
