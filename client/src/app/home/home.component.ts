import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  http = inject(HttpClient) // dependency injecttion 
  registerMode = false;
  users:any;

  ngOnInit(): void {
    this.getUsers()
  }

  registerToggle()
  {
    this.registerMode = !this.registerMode;

  }

  cancelRegisterMode(event: boolean)
  {
    this.registerMode = event;
  }

  getUsers()
  {
    this.http.get('http://localhost:5000/api/User').subscribe({  // observables and subscription 
      next: response => this.users = response, // executes when request is successfull
      error: error => console.log(error), // executes if  error comes 
      complete: () => console.log('Request has completed') // executes when api request is completed
    })
  }

}
