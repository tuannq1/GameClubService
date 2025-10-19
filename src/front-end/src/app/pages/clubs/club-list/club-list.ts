import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ClubService, Club } from '../../../services/club';

@Component({
  selector: 'app-club-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './club-list.html',
  styleUrls: ['./club-list.scss'], // optional
})
export class ClubList implements OnInit {
  clubs: Club[] = [];

  constructor(private clubService: ClubService) {}

  ngOnInit(): void {
    this.clubService.getClubs().subscribe({
      next: (data) => (this.clubs = data),
      error: (err) => console.error('Error fetching clubs', err),
    });
  }
}
