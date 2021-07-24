user_id_connect = null;
$(document).ready(function () {

    // global links :
    key = "0c4c09dc31abca426ccfff55b6d426c3";
    url = "https://api.themoviedb.org/3";
    imagePath = "https://image.tmdb.org/t/p/w500";
    api_key = "api_key=" + key;

    // log in status:
    if (localStorage["User"] != undefined) {
        user = JSON.parse(localStorage["User"]);
        category = user.Category;
        user_id_connect = user.User_id;
        Resethead(user.Role);
    }

    //read functions:
    GetPopularTV();
    GetRatedTV();
});
        //search TV Show:
function searchbutton() {
    localStorage["TVName"] = $("#search").val();
    searchValues();

    window.location.replace("../Pages/TVPage.html");
}
function GetPopularTV() {
    //Popular TV Shows:
    //https://api.themoviedb.org/3/tv/popular?api_key=0c4c09dc31abca426ccfff55b6d426c3
    let apiCall = url + "/tv/popular?" + api_key;
    ajaxCall("GET", apiCall, " ", getPopularTVSuccessCB, getPopularTVErrorCB);
    return false;
}

function GetRatedTV() {
    //Rating TV Show :
    //https://api.themoviedb.org/3/tv/top_rated?api_key=0c4c09dc31abca426ccfff55b6d426c3
    let apiCall = url + "/tv/top_rated?" + api_key;
    ajaxCall("GET", apiCall, " ", getTopRatedTVSuccessCB, getTopRatedTVErrorCB);
    return false;
}

function search(name) {
    localStorage["TVName"] = null;
    localStorage["TVName"] = name;
    searchValues();
    window.location.replace("../Pages/TVPage.html");

}

function searchValues() {
    TvnameSearch = localStorage["TVName"];

    let method = "/search/tv?";
    let moreParams = "&language=en-US&page=1&include_adult=false&";
    let query = "query=" + encodeURIComponent(TvnameSearch);
    let apiCall = url + method + api_key + moreParams + query;
    ajaxCall("GET", apiCall, " ", getSTVSuccessCB, getSTVErrorCB);
    return false;

}


function Resethead(role) {
    document.getElementById("login").style.display = "none";
    document.getElementById("signup").style.display = "none";
    document.getElementById("head").appendChild
    str = "<button id='logout' onclick='logout()'>Log Out</button>";
    if (role == "user") {
        str += "<a href='UserPage.html' id='userpage'>My Page</a>";
    }
    else {
        str += "<a href='AdminPage.html' id='adminpage'>My Page</a>";
    }
    document.getElementById("head").innerHTML += str;
}


function logout() {
    localStorage.clear();
    location.reload();
    window.location.replace("../Pages/HomePage.html");
}

function getPopularTVSuccessCB(popularTvArray) {
    poptv = popularTvArray.results;

    card = `<div id='multi-item' class='carousel slide' data-ride='carousel'>
        <div class='controls-top' style='text-align:center' >
            <a class='btn-floating' href='#multi-item' data-slide='prev'>
                <i class='fa fa-chevron-left  w3-xxxlarge' style='color:grey'></i>
            </a>
            <a class='btn-floating' href='#multi-item' data-slide='next'>
                <i class='fa fa-chevron-right w3-xxxlarge' style='color:grey'></i>
            </a>
        </div><br><br>

            <div class='carousel-inner' role='listbox'>`;

    for (var i = 0; i < poptv.length; i++) {
        if (i % 6 === 0)
            card += `<div class='carousel-item ${i === 0 ? ' active' : ""}'>
                                        <div class="row">`;

        card += `<div class='col-md-2' style='float:left'>
                        <div class='card mb-2' id="${poptv[i].id}" >
                            <img class='card-img-top' src="${imagePath + poptv[i].poster_path}" alt='Card image' />
                            <div class="c100  v${poptv[i].vote_average % 10 * 10} small ">
                                <span style='color:black;'>${poptv[i].vote_average % 10 * 10}%</span>
                                <div class="slice" style='background-color:black;border-radius:100%;'>
                                    <div class="bar"></div>
                                    <div class="fill"></div>
                                </div>
                            </div>
                            <p class='card-text'>${poptv[i].first_air_date}</p>
                            <div class='card-body'>
                                <h6 class='card-title'>${poptv[i].name}</h6>
                                <button value="${poptv[i].name}" onclick='search(this.value)' class='btn btn-primary'>See more</button>
                                <button value="${poptv[i].id}" onclick='likeTV(this.value)' class='btn btn-primary'>Like</button>
                            </div>
                        </div>
                    </div>`;
        if (i % 6 === 5)
            card += "</div></div>";

        tv = {
            Show_id: poptv[i].id,
            First_air_date: poptv[i].first_air_date,
            Show_name: poptv[i].name,
            Origin_country: poptv[i].origin_country[0],
            Original_language: poptv[i].original_language,
            Overview: poptv[i].overview,
            Popularity: poptv[i].popularity,
            Poster_path: poptv[i].poster_path,
            Likes: 0

        }
        let apitv = "../api/TvShow";
        ajaxCall("POST", apitv, JSON.stringify(tv), postTvshowSuccessCB, postTvshowErrorCB);


    }
    card += "</div></div>"
    $("#popdiv").html(card);
    $('.carousel').carousel()
}

function getPopularTVErrorCB(error) {

}

function getTopRatedTVSuccessCB(TopRatedTV) {
    toptv = TopRatedTV.results;
    card = `<div id='multi-item2' class='carousel slide' data-ride='carousel'>
            <div class='controls-top'>
                <a class='btn-floating' href='#multi-item2' data-slide='prev'>
                    <i class='fa fa-chevron-left' style='margin:auto 0px; background-color:green;'></i>
                </a>
                <a class='btn-floating' href='#multi-item2' data-slide='next'>
                    <i class='fa fa-chevron-right'></i>
                </a>
            </div><br><br>

                <div class='carousel-inner' role='listbox'>`;

    for (var i = 0; i < toptv.length; i++) {
        if (i % 6 === 0)
            card += `<div class='carousel-item ${i === 0 ? ' active' : ""}'>
                                        <div class="row">`;

            card += `<div class='col-md-2' style='float:left'>
                            <div class='card mb-2' id="${toptv[i].id}">
                                <img class='card-img-top' src="${imagePath + toptv[i].poster_path}" alt='Card image' />
                                <p class='card-text'>${toptv[i].first_air_date}</p>
                                <div class='card-body'>
                                    <h6 class='card-title'>${toptv[i].name}</h6>
                                    <button value="${toptv[i].name}" onclick='search(this.value)' class='btn btn-primary'>See more</button>
                                    <button value="${toptv[i].id}" onclick='likeTV(this.value)' class='btn btn-primary'>Like</button>
                               </div>
                            </div>
                        </div>`;
            if (i % 6 === 5)
                card += "</div></div>";
        tv = {
            Show_id: toptv[i].id,
            First_air_date: toptv[i].first_air_date,
            Show_name: toptv[i].name,
            Origin_country: toptv[i].origin_country[0],
            Original_language: toptv[i].original_language,
            Overview: toptv[i].overview,
            Popularity: toptv[i].popularity,
            Poster_path: toptv[i].poster_path,
            Likes: 0
        }
        let apitv = "../api/TvShow";
        ajaxCall("POST", apitv, JSON.stringify(tv), postTvshowSuccessCB, postTvshowErrorCB);
    }
    card += "</div></div>"
    $("#ratdiv").html(card);
    $('.carousel').carousel()

}

function likeTV(tv_id) {
    if (user_id_connect != null) {
        let api = "../api/TvShow?id=" + tv_id;
        ajaxCall("Get", api, "", FindTVSuccessCB, FindTVErrorCB)
        return false;
    }
    else alert("log in");

}

function FindTVSuccessCB(obj) {
    let api = "../api/TvShow?id=" + obj.Show_id + "&likes=" + obj.Likes;
    ajaxCall("Put", api, "", UpdateTVSuccessCB, UpdateTVErrorCB)
    return false;
}

function UpdateTVSuccessCB() {
}

function UpdateTVErrorCB(err) {
}

function FindTVErrorCB(err) {
}

function getTopRatedTVErrorCB(error) {
}

function getSTVSuccessCB(tv) {
    tvseries = tv.results[0];
    tvId = tv.results[0].id;
    let poster = imagePath + tvseries.poster_path;

    str = "<br><div id='moreinfo'><img id='poster' src='" + poster + "' />";
    str += "<p>" + tvseries.name + "</p>";
    srt += "<p>" + tvseries.original_language + "</p>";
    str += "<p>" + tvseries.overview + "</p>";
    str += "<p>" + tvseries.first_air_date + "</p>";
    str += "<p>" + tvseries.popularity + "</p>";
    str += "<p>" + tvseries.vote_average + "</p>";
    $("#ph").html(str);

    let method = "/tv/";
    let season = url + method + tvId + "?" + api_key;
    ajaxCall("GET", season, " ", getSeasonSuccessCB, getSeasonErrorCB);
    return false;

}

function getSTVErrorCB(error) { }

function getSeasonSuccessCB(season) {

    let seasonarray = season.seasons;
    for (var i = 0; i < seasonarray.length; i++) {
        str += `<button id='${seasonarray[i].season_number}' onclick="getSeasonNumber(this.id)"> season ${seasonarray[i].season_number}</button>`;
    }
    $("#ph").html(str);
}

function getSeasonNumber() {

    ///tv/{tv_id}/season/{season_number}/episode/{episode_number}
    let method = "/tv/";
    let number = $("#selected").val();
    let episodes = url + method + tvId + "/season/" + number + "?" + api_key;
    ajaxCall("GET", episodes, " ", getEpisodesSuccessCB, getEpisodesErrorCB);
    return false;
}

function getEpisodesSuccessCB(episodesforSeason) {
    let seasonarray = season.seasons;
    for (var i = 0; i < seasonarray.length; i++) {
        str += `<button id='${seasonarray[i].season_number}' onclick="getSeasonNumber(this.id)"> season ${seasonarray[i].season_number}</button>`;
    }
    $("#ph").html(str);
    addepisode = episodesforSeason;
    str1 = "<div>"
    for (var i = 0; i < episodesforSeason.episodes.length;i++) {
        n = i + 1;
        str1 += "<p>" + n + ".episode name : " + episodesforSeason.episodes[i].name + "<br><img src='" + imagePath + episodesforSeason.episodes[i].still_path + "' /><br> overview:" + episodesforSeason.episodes[i].overview + "<br> air_ date : " + episodesforSeason.episodes[i].air_date + "<br></p>";
        str1 += "<button value='" + episodesforSeason.episodes[i].id + "' onclick='favoritepisode(" + episodesforSeason.episodes[i].id + "," + tvId + ")'> add  </button>";
        episode = {
            Episode_name: episodesforSeason.episodes[i].name,
            Season_num: episodesforSeason.episodes[i].season_number,
            Img: episodesforSeason.episodes[i].still_path,
            Description: episodesforSeason.episodes[i].overview,
            Date: episodesforSeason.episodes[i].air_date,
            Episode_id: episodesforSeason.episodes[i].id,
            Show_id: tvId,
            Likes: 0
        }
        let apiep = "../api/Episode";
        ajaxCall("POST", apiep, JSON.stringify(episode), postEpisodeSuccessCB, postEpisodeErrorCB)
    }
            str1 += "</div>";
    $("#ph1").html(str1);

}

function getEpisodesErrorCB(err) {
    console.log(err);
}

function getSeasonErrorCB(err) {

    console.log(err);
}


function getTVErrorCB(err) {
    console.log(err);
}

function favoritepisode(episode_id, show_id) {
    user = JSON.parse(localStorage["User"]);
    favorite = {
        User_id: user.User_id,
        Episode_id: episode_id,
        TvShow_id: show_id
    }

    let api = "../api/Favorite";
    ajaxCall("POST", api, JSON.stringify(favorite), postFavoriteSuccessCB, postFavoriteErrorCB)
    return false;

}

function postTvshowSuccessCB() {
}

function postTvshowErrorCB(err) {
}

function postEpisodeSuccessCB() {
}

function postEpisodeErrorCB(err) {
}

function postFavoriteSuccessCB() {
}

function postFavoriteErrorCB(err) {
}
    