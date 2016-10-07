
$(document).ready(function () {
    loadPlanets();
});

function loadPlanets() {
    $.ajax({
        url: "api/planet", success: function (result) {
            console.log("Planetes loaded", result);

            $.each(result, function (index, item) {
                $("div#planet-list").append('<a href="#' + item.Id + '" data-state="0" data-id="' + item.Id + '" onclick="loadPlanet(this)" class="list-group-item">' + item.Name + '</a>');
            });
        }
    });
}

function loadPlanet(elem) {
    var self = $(elem);

    console.warn("Item ", self, "state is ", self.data("state"));

    if (self.data("state") === 0) {
        var id = self.data("id");
         
        self.addClass("active");
        self.data("state", 1);
        $.ajax({
            url: "api/planet/" + id,
            success: function (result) { 
                console.log("Load data ", result, " by id=", id);
 
                self.html(self.html() + "<p>" + numberWithCommas(result.KmFromSun) + " mln km from Sun</p>");
            }
        });
    } else {
        self.removeClass("active");
        self.find("p").remove();
        self.data("state", 0);
    }
}

function numberWithCommas(x) {
    return x.toLocaleString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}