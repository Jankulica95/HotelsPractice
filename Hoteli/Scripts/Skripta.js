$(document).ready(function () {

    var host = window.location.host;
    var port = "";
    var hotelsEndpoint = "/api/hotels/";
    var lanciHotelaEndpoint = "/api/lanachotelas/";
    var formAction = "Create";
    var editingId;
    var loggedUser = "";

    var token = null;

    var headers = {};


    $("body").on("click", "#btnDeleteHotel", deleteHotel);

    $("body").on("click", "#btnEditHotel", editHotel);

    $("#registracijabtn").on("click", registracijaFunc);
    $("#prijavabtn").on("click", prijavaFunc);
    $("#prijavaNaSistem").on("click", prijavaFunc);

    $("body").on("click", "#odustajanje", function myfunction(e) {
        e.preventDefault();
        $("#buttons").css("display", "block");
        $("#registracija").css("display", "none");
        $("#prijava").css("display", "none");
    });

    $("#formClear").on("click", clearForm);

    onLoad();

    // posto inicijalno nismo prijavljeni, sakrivamo odjavu
    $("#odjava").css("display", "none");

    function clearForm() {
        $("#hotelNaziv").val("");
        $("#hotelGodinaOsnivanja").val("");
        $("#hotelBrojSoba").val("");
        $("#hotelBrojZaposlenih").val("");
    }

    function registracijaFunc() {
        $("#registracija").css("display", "block");
        $("#buttons").css("display", "none");
        $("#prijava").css("display", "none");
    }

    function prijavaFunc() {
        $("#prijava").css("display", "block");
        $("#buttons").css("display", "none");
        $("#registracija").css("display", "none");
    }

    function onLoad() {

        var requestUrlHotel = "http://" + host + port + hotelsEndpoint;
        $.getJSON(requestUrlHotel, setHotels);

        var requestUrlLanac = "http://" + host + port + lanciHotelaEndpoint;
        $.getJSON(requestUrlLanac, function (data) {
            $("#hotelLanacHotela").empty();
            for (var i = 0; i < data.length; i++) {
                $("#hotelLanacHotela").append("<option value=" + data[i].Id + ">" + data[i].Naziv + "</option>")
            }
        });
    }

    // registracija korisnika
    $("#registracija").submit(function (e) {
        e.preventDefault();

        var email = $("#regEmail").val();
        var loz1 = $("#regLoz").val();
        var loz2 = $("#regLoz2").val();

        // objekat koji se salje
        var sendData = {
            "Email": email,
            "Password": loz1,
            "ConfirmPassword": loz2
        };
        $.ajax({
            type: "POST",
            url: 'http://' + host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {
            $("#info").append("Uspešna registracija. Možete se prijaviti na sistem.");
            $("#uspesnoRegistrovanje").css("display", "block");

        }).fail(function (data) {
            alert("Greška prilikom registracije");
        });


    });


    // prijava korisnika
    $("#prijava").submit(function (e) {
        e.preventDefault();

        var email = $("#priEmail").val();
        var loz = $("#priLoz").val();

        // objekat koji se salje
        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": loz
        };

        $.ajax({
            "type": "POST",
            "url": 'http://' + host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
            $("#info").empty().append("Prijavljen korisnik: " + data.userName);
            //sessionStorage.setItem("token", data.access_token);
            token = data.access_token;
            $("#prijava").css("display", "none");
            $("#registracija").css("display", "none");
            $("#filteritradicija").css("display", "block");
            $("#odjava").css("display", "block");
            onLoad();

        }).fail(function (data) {
            alert(data);
        });
    });

    $("#filterForm").submit(function (e) {
        e.preventDefault();

        var minSoba = $("#minSoba").val();
        var maxSoba = $("#maxSoba").val();

        // objekat koji se salje
        var sendData = {
            "MinSoba": minSoba,
            "MaxSoba": maxSoba,
        };

        $.ajax({
            "type": "POST",
            "url": 'http://' + host + port + "/api/kapacitet/",
            "data": sendData

        }).done(function (data, status) {
            console.log(data);
            setHotels(data, status)

        }).fail(function (data) {
            alert(data);
        });
    });

    $("#tradicijabtn").on("click", function () {
        requestUrlTradicija = "http://" + host + port + "/api/tradicija";
        $.getJSON(requestUrlTradicija, setTradicija);
    });

    function setTradicija(data, status) {
        var ol = $("<ol></ol>");
        for (var i = 0; i < data.length; i++) {
            ol.append("<li><b>" + data[i].Naziv + "</b>(osnovan je <b> " + data[i].GodinaOsnivanja + "</b>. godine) </li > ")
        }
        $("#tradicijaResult").append(ol);
        $("#tradicijabtn").css("display", "none");
    }

    // odjava korisnika sa sistema
    $("#odjavise").click(function () {
        //sessionStorage.removeItem("token");
        token = null;
        headers = {};
        $("#buttons").css("display", "block");
        $("#prijava").css("display", "none");
        $("#registracija").css("display", "none");
        $("#odjava").css("display", "none");
        $("#filteritradicija").css("display", "none");
        $("#info").empty();
        onLoad();

    })

    // ucitavanje prvog proizvoda
    $("#proizvodi").click(function () {
        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        $.ajax({
            "type": "GET",
            "url": "http://" + host + "/api/products/1",
            "headers": headers

        }).done(function (data) {
            $("#sadrzaj").append("Proizvod: " + data.Name);

        }).fail(function (data) {
            alert(data.status + ": " + data.statusText);
        });


    });

    function setHotels(data, status) {
        console.log("Status: " + status);

        var $container = $("#dataHoteli");
        $container.empty();

        if (status == "success") {
            console.log(data);
            // ispis naslova
            var div = $("<div></div>");
            var h2 = $("<h2>Hoteli</h2>");
            div.append(h2);
            // ispis tabele
            var table = $("<table border=3 class='table table - responsive'></table>");
            var header = $("<tr><th>Naziv</th><th>Godina Otvaranja</th><th>Broj Soba</th><th>Broj zaposlenih</th><th>Lanac</th></tr>");
            var headerLogged = $("<tr><th>Naziv</th><th>Godina Otvaranja</th><th>Broj Soba</th><th>Broj zaposlenih</th><th>Lanac</th><th>Brisanje</th><th>Izmena</th></tr>");

            if (token) {
                table.append(headerLogged);
            }
            else {
                table.append(header);
            }

            for (i = 0; i < data.length; i++) {
                // prikazujemo novi red u tabeli
                var row = "<tr>";
                // prikaz podataka
                var displayData = "<td>" + data[i].Naziv + "</td><td>" + data[i].GodinaOsnivanja + "</td><td>" + data[i].BrojSoba + "</td><td>" + data[i].BrojZaposlenih + "</td><td>" + data[i].LanacHotelaNaziv + "</td>";

                if (token) {
                    var stringId = data[i].Id.toString();
                    var displayDelete = "<td><button id=btnDeleteHotel name=" + stringId + ">Delete</button></td>";
                    var displayEdit = "<td><button id=btnEditHotel name=" + stringId + ">Edit</button></td>";
                    row += displayData + displayDelete + displayEdit + "</tr>";
                }
                else {
                    row += displayData + "</tr>";
                }
                table.append(row);

                div.append(table);

                // prikaz forme
                if (token) {
                    $("#formDivHotel").css("display", "block");
                }
                else {
                    $("#formDivHotel").css("display", "none");
                }

                // ispis novog sadrzaja
                $container.append(div);
            }
        }
        else {
            var div = $("<div></div>");
            var h1 = $("<h1>Greška prilikom preuzimanja hotela!</h1>");
            div.append(h1);
            $container.append(div);
        }
    };

    function deleteHotel() {
        // izvlacimo {id}
        var deleteID = this.name;
        // saljemo zahtev 
        $.ajax({
            url: "http://" + host + port + hotelsEndpoint + deleteID.toString(),
            type: "DELETE",
        })
            .done(function (data, status) {
                refreshTable();
            })
            .fail(function (data, status) {
                alert("Desila se greska!");
            });

    };

    // izmena drzave
    function editHotel() {
        // izvlacimo id
        var editId = this.name;

        if (token) {
            headers.Authorization = "Bearer " + token;
        }
        // saljemo zahtev da dobavimo tog autora
        $.ajax({
            url: "http://" + host + port + hotelsEndpoint + editId.toString(),
            type: "GET",
            headers: headers
        })
            .done(function (data, status) {
                $("#hotelNaziv").val(data.Naziv);
                $("#hotelGodinaOsnivanja").val(data.GodinaOsnivanja);
                $("#hotelBrojSoba").val(data.BrojSoba);
                $("#hotelBrojZaposlenih").val(data.BrojZaposlenih);

                $("#hotelLanacHotela option").each(function () {
                    this.removeAttribute('selected');
                });
                $("#hotelLanacHotela option").each(function () {
                    this.setAttribute('selected', '');
                    if (this.value == data.LanacHotelaId) {
                        this.setAttribute('selected', 'selected');
                        return false;
                    }
                });

                editingId = data.Id;
                formAction = "Update";
            })
            .fail(function (data, status) {
                formAction = "Create";
                alert("Desila se greska!");
            });

    };

    $("#hotelForm").submit(function (e) {
        // sprecavanje default akcije forme
        e.preventDefault();
        var result = validacija();

        if (!result) {
            return false;
        }

        var hotelNaziv = $("#hotelNaziv").val();
        var hotelGodinaOsnivanja = $("#hotelGodinaOsnivanja").val();
        var hotelBrojSoba = $("#hotelBrojSoba").val();
        var hotelBrojZaposlenih = $("#hotelBrojZaposlenih").val();
        var hotelLanacHotelaId = $("#hotelLanacHotela").val();

        var httpAction;
        var sendData;
        var url;

        // u zavisnosti od akcije pripremam objekat
        if (formAction === "Create") {
            httpAction = "POST";
            url = "http://" + host + port + hotelsEndpoint;
            sendData = {
                "Naziv": hotelNaziv,
                "GodinaOsnivanja": hotelGodinaOsnivanja,
                "BrojSoba": hotelBrojSoba,
                "BrojZaposlenih": hotelBrojZaposlenih,
                "LanacHotelaId": hotelLanacHotelaId
            };
        }
        else {
            httpAction = "PUT";
            url = "http://" + host + port + hotelsEndpoint + editingId.toString();
            sendData = {
                "Id": editingId,
                "Naziv": hotelNaziv,
                "GodinaOsnivanja": hotelGodinaOsnivanja,
                "BrojSoba": hotelBrojSoba,
                "BrojZaposlenih": hotelBrojZaposlenih,
                "LanacHotelaId": hotelLanacHotelaId
            };
        }


        console.log("Objekat za slanje");
        console.log(sendData);

        $.ajax({
            url: url,
            type: httpAction,
            data: sendData
        })
            .done(function (data, status) {
                formAction = "Create";
                clearForm();
                refreshTable();
            })
            .fail(function (data, status) {
                alert("Desila se greska!");
            })

    });

    function refreshTable() {
        // cistim formu
        var requestUrlHotel = "http://" + host + port + hotelsEndpoint;
        $.getJSON(requestUrlHotel, setHotels);
    };
});