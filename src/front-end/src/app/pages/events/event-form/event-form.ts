import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EventService, Event } from '../../../services/event';

@Component({
  selector: 'app-event-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './event-form.html',
  styleUrls: ['./event-form.scss'],
})
export class EventForm {
  event: Event = {
    clubId: '',
    title: '',
    description: '',
    scheduledAt: '',
  };

  constructor(private eventService: EventService) {}

  createEvent() {
    this.eventService.createEvent(this.event).subscribe({
      next: () => alert('Event created successfully!'),
      error: (err) => console.error('Error creating event', err),
    });
  }
}
