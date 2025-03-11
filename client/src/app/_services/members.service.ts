import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Member } from '../_models/member';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient)

  baseUrl =  environment.apiUrl

  getMembers() 
  {
    return this.http.get<Member[]>(this.baseUrl + 'User');
  }
  getMember(username:string) 
  {
    return this.http.get<Member>(this.baseUrl + 'User/' + username);
  }


}
