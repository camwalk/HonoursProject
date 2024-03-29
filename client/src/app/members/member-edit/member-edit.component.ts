import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Instrument } from 'src/app/_models/instrument';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  member: Member;
  user: User;
  dropdownOptions = [
    {value: 'Casual', display: 'Casual'},
    {value: 'Professional', display: 'Professional'}
  ];
  preferredInstruments: Instrument[] = [];

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private accountService: AccountService, private memberService: MembersService, private toastr: ToastrService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    this.memberService.getMember(this.user.username).subscribe(member => {
      this.member = member;
      this.preferredInstruments = this.member.preferredInstruments;
    })
  }

  updateMember() {
    this.memberService.updateMember(this.member).subscribe(() => {
      this.toastr.success('Profile updated successfully')
      this.editForm.reset(this.member);
    })
  }

  addInstrument(newInstrument: string){
    if (newInstrument.length > 10){
      this.toastr.error('Instrument name is too long')
    }
    else{
      this.memberService.addInstrument(newInstrument).subscribe(() => {
        this.toastr.success('Instrument added successfully')
        this.editForm.reset(this.member);
        window.location.reload();
      })
    }
  }

  removeInstrument(instrumentName) {
    this.memberService.deleteInstrument(instrumentName).subscribe(() => {
      this.toastr.success('Instrument removed successfully')
      this.editForm.reset(this.member);
      window.location.reload();
    })
  }
}
