import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DirectMessage } from '../_models/directMessage';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class DirectMessageService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getDirectMessages(pageNumber, pageSize, container) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<DirectMessage[]>(this.baseUrl + 'directMessages',params, this.http);
  }

  getDirectMessageThread(username: string) {
    return this.http.get<DirectMessage[]>(this.baseUrl + 'directMessages/thread/' + username);
  }

  sendDirectMessage(username: string, messageContent: string) {
    return this.http.post<DirectMessage>(this.baseUrl + 'directMessages', {recieverUsername: username, messageContent})
  }

  deleteDirectMessage(id: number) {
    return this.http.delete(this.baseUrl + 'messages/' + id);
  }
}
