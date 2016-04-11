$(function () {
    var newMovieBtn = $("#newMovieBtn");
    var movieNameInput = $("#movieName");
    var movieList = $("#moviesList");

    movieList.on("click", "button", function () {
        $(this).closest("li").slideUp(function () {
            $(this).detach();
        });
    });

    newMovieBtn.click(function () {
        var movieName = movieNameInput.val();
        if (movieName == "")
            return;
        var newMvie = { Title: movieName, Description: "aprox. o descriere" };
        $.post("/api/Movies", newMvie, function (data) { });

        addMovie(movieName);
        
        movieNameInput.val("").focus();
    });

    $("#loadMoviesBtn").click(function () {
        $.getJSON("/api/Movies", "", function (data) {            
            $(data).each(function () {
                addMovie(this.Title);
            })
        });
    }).click();

    function addMovie(movieName) {
        var newLi = $("<li />").html(movieName).hide();
        var removeBtn = $("<button />").html("X");
        newLi.append(removeBtn);
        movieList.append(newLi);
        newLi.slideDown();
    }
});

