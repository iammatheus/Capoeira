import { environment } from './../../../environments/environment.prod';
import { Component, ElementRef, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-carrousel',
  templateUrl: './carrousel.component.html',
  styleUrls: ['./carrousel.component.scss']
})
export class CarrouselComponent implements OnInit {

  @Input() titulo: string;
  @Input() itens: any;
  @Input() dadoSelecionado: any;
  @Input() id: any;

  constructor(
    public element: ElementRef
  ) { }

  ngOnInit() {
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
  public prev($event: any, idContainer: string, arrayItems: any){
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
