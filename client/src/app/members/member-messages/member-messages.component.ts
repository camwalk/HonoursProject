import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DirectMessage } from 'src/app/_models/directMessage';
import { DirectMessageService } from 'src/app/_services/direct-message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @ViewChild('messageForm') messageForm: NgForm;
  @Input() directMessages: DirectMessage[];
  @Input() username: string;
  messageContent: string;

  constructor(private directMessageService: DirectMessageService) { }

  ngOnInit(): void {
  }

  sendDirectMessage() {
    this.directMessageService.sendDirectMessage(this.username, this.messageContent).subscribe(directMessage =>{
      this.directMessages.push(directMessage);
      this.messageForm.reset();
    })
  }
}
