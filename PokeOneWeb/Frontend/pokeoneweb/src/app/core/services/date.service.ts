import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class DateService {
    public convertDate(date: String) {
        let dd = (date.split(" ")[1].split(",")[0]).trim().padStart(2, '0');
        let month = (date.split(" ")[0]).trim().padStart(2, '0');
        let yyyy = (date.split(", ")[1]).trim();

        let monthNames = ["Jan", "Feb", "Mar", "Apr",
            "May", "Jun", "Jul", "Aug",
            "Sep", "Oct", "Nov", "Dec"];

        let mm = monthNames.indexOf(month) + 1; //January is 0!

        return dd + '/' + mm + '/' + yyyy;
    }

    public getTodaysDate() {
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();

        return dd + '/' + mm + '/' + yyyy;
    }
}