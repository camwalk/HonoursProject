import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { DirectMessage } from 'src/app/_models/directMessage';
import { Instrument } from 'src/app/_models/instrument';
import { Member } from 'src/app/_models/member';
import { DirectMessageService } from 'src/app/_services/direct-message.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs', { static: true }) memberTabs: TabsetComponent;
  currentTab: TabDirective;
  member: Member;
  preferredInstrument: Instrument;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  directMessages: DirectMessage[] = [];
  preferredInstruments: Instrument[] = [];

  constructor(private memberService: MembersService, private route: ActivatedRoute, private directMessageService: DirectMessageService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.member = data.member;
    })

    this.route.queryParams.subscribe(params => {
      params.tab ? this.selectTab(params.tab) : this.selectTab(0);
    })

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]

    this.galleryImages = this.getImages();

    this.preferredInstruments = this.member.preferredInstruments;
  }

  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    for (const photo of this.member.photos) {
      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      })
    }
    return imageUrls;
  }

  loadDirectMessages() {
    this.directMessageService.getDirectMessageThread(this.member.username).subscribe(directMessages => {
      this.directMessages = directMessages;
    })
  }

  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }

  onTabActivated(data: TabDirective) {
    this.currentTab = data;
    if (this.currentTab.heading === 'Messages' && this.directMessages.length === 0) {
      this.loadDirectMessages();
    }
  }
}
