import { Mestre } from './../../models/Mestre';
import { environment } from 'src/environments/environment.prod';
import { HomeService } from './../../services/home.service';
import { Component, ElementRef, OnInit } from '@angular/core';
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

  public largImg = 100;
  public altImg = 75;
  public margemImg = 2;
  public exibirImg = true;
  public dado: any;
  public loading: any;
  public dadoSelecionado: any;

  constructor(
    private homeService: HomeService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private element: ElementRef
  ) { }

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

  public mostraImagem(imagemURL: string): string {
    return (imagemURL !== '')
    ? `${environment.apiURL}resources/images/${imagemURL}`
    : 'assets/img/sem-imagem.png';
  }

  public setImagemSlide(dado: any){
    this.dadoSelecionado = dado;
  }

  currentItem = -1;
  prev($event: any, idContainer: string, arrayItems: any){
    const elements = this.element.nativeElement.querySelectorAll(idContainer);
    let isArrow = $event.target.classList.contains("setaLeft");
    if(isArrow){
      this.currentItem -= 1;
    }else {
      this.currentItem += 1;
    }

    if(this.currentItem >= arrayItems.length){
      this.currentItem = 0;
    }

    if (this.currentItem < 0) {
      this.currentItem = arrayItems.length - 1;
    }
    this.setImagemSlide(arrayItems[this.currentItem]);

    elements.forEach((item: any) => item.classList.remove("current-item"));

    elements[this.currentItem].scrollIntoView({
      behavior: "smooth",
      inline: "center",
      block: "center"
    });
    elements[this.currentItem].classList.add("current-item");
  };
}


