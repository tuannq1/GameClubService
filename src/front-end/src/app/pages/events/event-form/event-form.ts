import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EventService, Event } from '../../../services/event';
import { ClubService, Club } from '../../../services/club';

@Component({
  selector: 'app-event-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './event-form.html',
  styleUrls: ['./event-form.scss'],
})
export class EventForm implements OnInit{
  event: Event = {
    clubId: '',
    title: '',
    description: '',
    scheduledAt: '',
  };
  clubs: Club[] = [];

  constructor(private eventService: EventService, private clubService: ClubService) {}

  ngOnInit() {
    this.clubService.getClubs().subscribe({
      next: (data) => (this.clubs = data),
      error: (err) => console.error('Error fetching clubs', err),
    });
  }

  createEvent() {
    console.log(this.event);
    this.eventService.createEvent(this.event).subscribe({
      next: () => alert('Event created successfully!'),
      error: (err) => console.error('Error creating event', err),
    });
  }
}
