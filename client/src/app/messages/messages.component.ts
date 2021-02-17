import { Component, OnInit } from '@angular/core';
import { DirectMessage } from '../_models/directMessage';
import { Pagination } from '../_models/pagination';
import { DirectMessageService } from '../_services/direct-message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  directMessages: DirectMessage[] = [];
  pagination: Pagination;
  container = 'Unread';
  pageNumber = 1;
  pageSize = 5;
  loading = false;

  constructor(private directMessageService: DirectMessageService) { }

  ngOnInit(): void {
    this.loadDirectMessages();
  }

  loadDirectMessages() {
    this.loading = true;
    this.directMessageService.getDirectMessages(this.pageNumber, this.pageSize, this.container).subscribe(response => {
      this.directMessages = response.result;
      this.pagination = response.pagination;
      this.loading = false;
    }
    )
  }

  deleteDirectMessage(id: number) {
    this.directMessageService.deleteDirectMessage(id).subscribe(() => {
      this.directMessages.splice(this.directMessages.findIndex(m => m.id === id), 1);
    })
  }

  pageChanged(event:any) {
    this.pageNumber = event.page;
    this.loadDirectMessages();
  }
}
