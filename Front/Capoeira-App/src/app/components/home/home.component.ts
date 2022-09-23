import { Mestre } from './../../models/Mestre';
import { HomeService } from './../../services/home.service';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Evento } from '@app/models/Evento';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { environment } from './../../../environments/environment.prod';
import SwiperCore, { FreeMode, Navigation, Thumbs, Pagination } from "swiper";
SwiperCore.use([FreeMode, Navigation, Thumbs, Pagination]);
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HomeComponent implements OnInit {

  public eventos: Evento[] = [];
  public mestres: Mestre[] = [];
  thumbsSwiper: any;

  constructor(
    private homeService: HomeService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
  ) {  }

  ngOnInit(): void {
    this.carregaEventos();
    this.carregaMestres();
  }

  public mostraImagem(imagemURL: string): string {
    return (imagemURL !== '')
    ? `${environment.apiURL}resources/images/${imagemURL}`
    : 'assets/img/sem-imagem.png';
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


