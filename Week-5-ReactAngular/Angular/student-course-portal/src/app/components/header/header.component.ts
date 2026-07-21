import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  template: `
    <header style="padding: 1rem; background: #eee;">
      <h2>Student Course Portal</h2>
      <nav style="display: flex; gap: 1rem;">
        <a routerLink="/" routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}">Home</a>
        <a routerLink="/courses" routerLinkActive="active">Courses</a>
        <a routerLink="/profile" routerLinkActive="active">Profile</a>
      </nav>
    </header>
  `
})
export class HeaderComponent {}
