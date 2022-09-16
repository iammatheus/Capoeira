import { Mestre } from './../../models/Mestre';
import { HomeService } from './../../services/home.service';
import { Component, OnInit } from '@angular/core';
import { Evento } from '@app/models/Evento';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public eventos: Evento[] = [];
  public mestres: Mestre[] = [];

  constructor(
    private homeService: HomeService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
  ) {  }

  ngOnInit(): void {
    this.carregaEventos();
    this.carregaMestres();
  }

  public carregaEventos(): void {
    this.spinner.show();
    this.homeService.getEventos()
    .subscribe((eventos: Evento[])=> {
      this.eventos = eventos;
     },
     error => {
      this.spinner.hide();
      this.toastr.error('Erro ao carregar os eventos.', 'Erro!')
      console.error(error);
    },
    () => this.spinner.hide()
    )
  }

  public carregaMestres(): void {
    this.spinner.show();
    this.homeService.getMestres()
    .subscribe((mestres: Mestre[])=> {
      this.mestres = mestres;
     },
     error => {
      this.spinner.hide();
      this.toastr.error('Erro ao carregar os mestres.', 'Erro!')
      console.error(error);
    },
    () => this.spinner.hide()
    )
  }
}


