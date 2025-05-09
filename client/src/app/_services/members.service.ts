import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Member } from '../_models/member';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { AccountService } from './account.service';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient)

  baseUrl =  environment.apiUrl

  members = signal<Member[]>([]);

  getMembers() 
  {
    return this.http.get<Member[]>(this.baseUrl + 'User').subscribe ({
      next: members => this.members.set(members)
    })
  }
  getMember(username:string) 
  {
    const member = this.members().find(x => x.username == username);
    if (member != undefined) return of(member);

    return this.http.get<Member>(this.baseUrl + 'User/' + username);
  }

  updateMember(member: Member)
  {
    return this.http.put(this.baseUrl + 'User', member).pipe(
      tap(() => {
        this.members.update(members => members.map(m => m.username == member.username ? member : m))
      })
    )
  }
}
