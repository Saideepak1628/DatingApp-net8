import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  http = inject(HttpClient) // dependency injecttion 
  title = 'Dating App';
  users:any;

  // implementing hook that is initialted with the componenet implements API call will happen when component loads.
  ngOnInit(): void {
    this.http.get('http://localhost:5000/api/User').subscribe({  // observables and subscription 
      next: response => this.users = response, // executes when request is successfull
      error: error => console.log(error), // executes if  error comes 
      complete: () => console.log('Request has completed') // executes when api request is completed
    })
  }

}
