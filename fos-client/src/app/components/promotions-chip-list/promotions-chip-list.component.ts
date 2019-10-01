import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Promotion } from 'src/app/models/promotion';
import { PromotionType } from 'src/app/models/promotion-type';

@Component({
  selector: 'app-promotions-chip-list',
  templateUrl: './promotions-chip-list.component.html',
  styleUrls: ['./promotions-chip-list.component.less']
})
export class PromotionsChipListComponent implements OnInit {
  constructor() { }
  @Output() promotionChanged: EventEmitter<Promotion[]> = new EventEmitter();

  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;

  promotions: Promotion[] = [];
  promotionOptions: Promotion[] = [];
  promotionType: PromotionType = PromotionType.DiscountAll;
  promotionValue: string = '10';

  ngOnInit() {
    for (let index = 0; index < 3; index++) {
      const promotion = new Promotion();
      promotion.PromotionType = index;
      promotion.IsPercent = !(index === 2);
      this.promotions.push(promotion);
      this.promotionOptions.push(promotion);
    }
  }

  getPromotionName(promotionType: number): string {
    return PromotionType[promotionType];
  }

  addToPromotions() {
    const promotion = new Promotion();
    promotion.PromotionType = this.promotionType;
    promotion.Value = Number(this.promotionValue);
    this.promotions.push(promotion);
    this.promotionChanged.emit(this.promotions);
    // console.log(this.promotions);
  }

  removePromotion(promotion: Promotion) {
    this.promotions = this.promotions.filter(pr => pr !== promotion);
    this.promotionChanged.emit(this.promotions);
  }

}
