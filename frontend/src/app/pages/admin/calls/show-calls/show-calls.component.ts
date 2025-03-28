import { Component, OnInit } from '@angular/core';
import { MainNavbarComponent } from "../../../../components/main-navbar/main-navbar.component";
import { TopUserInfosComponent } from "../../../../components/top-user-infos/top-user-infos.component";
import { CallsTableComponent } from "../../../../components/calls-table/calls-table.component";
import { CallService } from '../../../../services/call.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CallTable } from '../../../../types/Call';
import { HttpResponse } from '@angular/common/http';
import { formatDate } from '../../../../utils/FormatData';

@Component({
  selector: 'app-show-calls',
  standalone: true,
  imports: [MainNavbarComponent, TopUserInfosComponent, CallsTableComponent],
templateUrl: './show-calls.component.html',
  styleUrl: './show-calls.component.sass'
})
export class ShowCallsComponent implements OnInit {
  title: string = 'Chamados';
  calls: CallTable[] = [];

  constructor(
    private callService: CallService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.callService.getAll().subscribe({
      next: (response: HttpResponse<CallTable[]>) => {
        this.calls = (response.body ?? []).map(call => {
          call.createdAt = formatDate(call.createdAt); 
          return call;
        });
      },
      error: (error) => {

      }

    })
  }
}
