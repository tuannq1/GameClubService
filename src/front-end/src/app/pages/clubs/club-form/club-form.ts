import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClubService, Club } from '../../../services/club';

@Component({
  selector: 'app-club-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './club-form.html',
  styleUrls: ['./club-form.scss']
})
export class ClubForm {
  club: Club = { id: '', name: '', description: '' };

  constructor(private clubService: ClubService) {}

  createClub() {
    this.clubService.createClub(this.club).subscribe({
      next: () => alert('Club created successfully!'),
      error: (err) => console.error('Error creating club', err),
    });
  }
}
