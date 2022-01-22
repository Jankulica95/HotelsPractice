 function validacija() {
        var hotelNaziv = document.getElementById("hotelNaziv").value;
        var hotelNazivGreska = document.getElementById("hotelNazivGreska");
        hotelNazivGreska.innerHTML = "";


        var hotelGodinaOsnivanja = document.getElementById("hotelGodinaOsnivanja").value;
        var hotelGodinaOsnivanjaGreska = document.getElementById("hotelGodinaOsnivanjaGreska");
        hotelGodinaOsnivanjaGreska.innerHTML = "";


        var hotelBrojSoba = document.getElementById("hotelBrojSoba").value;
        var hotelBrojSobaGreska = document.getElementById("hotelBrojSobaGreska");
        hotelBrojSobaGreska.innerHTML = "";

        var hotelBrojZaposlenih = document.getElementById("hotelBrojZaposlenih").value;
        var hotelBrojZaposlenihGreska = document.getElementById("hotelBrojZaposlenihGreska");
        hotelBrojZaposlenihGreska.innerHTML = "";

        var isValid = true;
        if (hotelNaziv === "" || hotelNaziv === undefined) {
            hotelNazivGreska.innerHTML = "<span class=\"greska\">Naziv nije unesen</span>"
            isValid = false;
        }
        else if (hotelNaziv.length > 80) {
            hotelNazivGreska.innerHTML = "<span class=\"greska\">Naziv hotela ne sme biti veci od 80 karaktera. </span>"
            isValid = false;
        }

     if (hotelGodinaOsnivanja === "" || hotelGodinaOsnivanja === undefined) {
         hotelGodinaOsnivanjaGreska.innerHTML = "<span class=\"greska\">Godina otvaranja hotela nije unesena.</span>"
         isValid = false;
     }
     else if (Number(hotelGodinaOsnivanja) <= 1950 || Number(hotelGodinaOsnivanja >=2020)) {
         hotelGodinaOsnivanjaGreska.innerHTML = "<span class=\"greska\">Godina otvarnja hotela je u rangu od 1950 do 2020 godine. </span>"
         isValid = false;
     }

     if (hotelBrojSoba === "" || hotelBrojSoba === undefined) {
         hotelBrojSobaGreska.innerHTML = "<span class=\"greska\">Broj soba u hotelu nije unesen.</span>"
         isValid = false;
     }
     else if (Number(hotelBrojSoba) < 9 || Number(hotelBrojSoba) > 1000) {
         hotelBrojSobaGreska.innerHTML = "<span class=\"greska\">Naziv hotela ne sme biti veci od 80 karaktera. </span>"
         isValid = false;
     }

     if (hotelBrojZaposlenih === "" || hotelBrojZaposlenih === undefined) {
         hotelBrojZaposlenihGreska.innerHTML = "<span class=\"greska\">Broj zaposlenih nije unesen.</span>"
         isValid = false;
     }
     else if (Number(hotelBrojZaposlenih) < 1) {
         hotelBrojZaposlenihGreska.innerHTML = "<span class=\"greska\">Naziv hotela ne sme biti veci od 80 karaktera. </span>"
         isValid = false;
     }

        return isValid;
 }
