import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit { 
  http = inject(HttpClient);
  title = 'Pets Alone';
  pets: any;

  ngOnInit(): void {
    this.http.get('https://localhost:7009/api/pets').subscribe({  
      next: response => this.pets = response,
      error: error => console.log(error),
      complete: () => {}
    });
  }
}
