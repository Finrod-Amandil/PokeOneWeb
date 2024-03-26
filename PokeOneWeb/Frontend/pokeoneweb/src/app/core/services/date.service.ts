import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class DateService {
    public convertDate(date: string) {
        const dd = date.split(' ')[1].split(',')[0].trim().padStart(2, '0');
        const month = date.split(' ')[0].trim();
        const yyyy = date.split(', ')[1].trim();

        const monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

        const mm = (monthNames.indexOf(month) + 1).toString().padStart(2, '0'); //January is 0!

        return dd + '/' + mm + '/' + yyyy;
    }

    public getTodaysDate() {
        const today = new Date();
        const dd = String(today.getDate()).padStart(2, '0');
        const mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        const yyyy = today.getFullYear();

        return dd + '/' + mm + '/' + yyyy;
    }
}
