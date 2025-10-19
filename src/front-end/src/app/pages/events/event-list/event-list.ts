import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventService, Event, } from '../../../services/event';
import { RouterModule } from '@angular/router';
import { ClubService, Club } from '../../../services/club';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './event-list.html',
  styleUrls: ['./event-list.scss'],
})
export class EventList implements OnInit {
  events: Event[] = [];
  clubs: Club[] = [];
  selectedClubId: string = '';
  constructor(private eventService: EventService, private clubService: ClubService) {}

  ngOnInit() {
    this.clubService.getClubs().subscribe({
      next: (data) => (this.clubs = data),
      error: (err) => console.error('Error fetching clubs', err),
    });
  }

  onClubChange(clubId: string) {
    this.eventService.getEvents(clubId).subscribe({
      next: (data) => (this.events = data),
      error: (err) => console.error('Error fetching events', err),
    });
  }
}
