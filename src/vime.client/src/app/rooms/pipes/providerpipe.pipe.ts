import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'providerPipe',
})
export class ProviderpipePipe implements PipeTransform {
  transform(providerNumber: number): string {
    switch (providerNumber) {
      case 0:
        return 'YouTube';
      case 1:
        return 'Vimeo';
      default:
        return 'Other';
    }
  }
}
