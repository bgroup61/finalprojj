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
        let api = "../api/User?email=" + user.Email + "&password=" + user.Password;
        ajaxCall("GET", api, "", getuserSuccessCB, getuserErrorCB)
    }

    //get tv by name:
    if (localStorage["TVName"] != undefined) {
        nametv = localStorage["TVName"];
        let method = "/search/tv?";
        let moreParams = "&language=en-US&page=1&include_adult=false&";
        let query = "query=" + encodeURIComponent(nametv);
        let apiCall = url + method + api_key + moreParams + query;
        ajaxCall("GET", apiCall, " ", getSTVSuccessCB, getSTVErrorCB);
    }

});

function getuserSuccessCB(us) {
    userconnected = us;
    Resethead();
}

function getuserErrorCB() {

}

function logout() {
    localStorage.clear();
    location.reload();
    window.location.replace("../Pages/HomePage.html");
}

function getSTVSuccessCB(tv) {
    tvseries = tv.results[0];
    tvId = tv.results[0].id;
    let poster = imagePath + tvseries.poster_path;

    str = `<br>
                  <div id='moreinfo' class='row' >
                  <div class='column'>
                  <img id='img' src="${poster}" />
                  </div>
                  <div class='column'>
                    <p>${tvseries.name}</p>
                    <p>${tvseries.original_language}</p>
                    <p>${tvseries.overview}</p>
                    <p>${tvseries.first_air_date}</p>
                    <p>${tvseries.popularity}</p>
                    <p>${tvseries.vote_average}</p>
                  </div>
                  </div > `;
    $("#ph").html(str);

    randerCast(tvId);
    let method = "/tv/";
    let season = url + method + tvId + "?" + api_key;
    ajaxCall("GET", season, " ", getSeasonSuccessCB, getSeasonErrorCB);
    return false;

}

function randerCast(tvId) {
    //https://api.themoviedb.org/3/tv/63714/aggregate_credits?api_key=0c4c09dc31abca426ccfff55b6d426c3
    casturl = url + "/tv/" + tvId + "/aggregate_credits?" + api_key;
    ajaxCall("GET", casturl, " ", getCastSuccessCB, getCastErrorCB);
    return false;
}

function getCastSuccessCB(result) {

    casts = result.cast;
    caststr = `<div id='multi-item3' class='carousel slide' data-ride='carousel'>
            <div class='controls-top' style='text-align:center;margin-top:80px'>
                <a class='btn-floating' href='#multi-item3' data-slide='prev'>
                    <i class='fa fa-chevron-left w3-xxxlarge' style='color:lightskyblue'></i>
                </a>
                <a class='btn-floating' href='#multi-item3' data-slide='next'>
                    <i class='fa fa-chevron-right w3-xxxlarge'  style='color:lightskyblue'></i>
                </a>
            </div><br><br>

                <div class='carousel-inner' role='listbox'>`;


    for (var i = 0; i < casts.length; i++) {
        if (i % 6 === 0)
            caststr += `<div class='carousel-item ${i === 0 ? ' active' : ""}'>
                                        <div class="row">`;

        caststr += `<div class='col-md-2' style='float:left'>
                            <div class='card mb-2'>
                                <img class='card-img-top' src="${imagePath + casts[i].profile_path}" alt='Card image' />
                                <p class='card-text'>${casts[i].name}</p>
                                <div class='card-body'>
                                    <h6 class='card-title'>${casts[i].total_episode_count}</h6>
                                    <button onclick='seeactor(${casts[i].id})'>see more</button>
                                </div>
                            </div>
                        </div>`;
        if (i % 6 === 5)
            caststr += "</div></div>";


    }
    caststr += "</div></div>"
    $("#castdiv").html(caststr);
    $('.carousel').carousel()
}

function seeactor(actorId) {
    localStorage["ActorId"] = actorId;
    window.location.replace("../Pages/ActorPage.html");

}



function getCastErrorCB(err) {

}

function getSeasonSuccessCB(season) {

    let seasonarray = season.seasons;
    for (var i = 0; i < seasonarray.length; i++) {

        if (seasonarray[i].season_number != 0)
            str += `<button class='btn' id='${seasonarray[i].season_number}' onclick="getSeasonNumber(this.id)"> season ${seasonarray[i].season_number}</button>`;
    }
    $("#ph").html(str);

}

function getSeasonNumber(number) {

    ///tv/{tv_id}/season/{season_number}/episode/{episode_number}
    let method = "/tv/";
    let episodes = url + method + tvId + "/season/" + number + "?" + api_key;
    ajaxCall("GET", episodes, " ", getEpisodesSuccessCB, getEpisodesErrorCB);
    return false;
}

function getEpisodesSuccessCB(episodesforSeason) {

    addepisode = episodesforSeason;
    str1 = "<div>"
    for (var i = 0; i < episodesforSeason.episodes.length; i++) {
        n = i + 1;
        str1 += "<p id='detailes'>" + n + ".episode name : " + episodesforSeason.episodes[i].name + "<br><img src='" + imagePath + episodesforSeason.episodes[i].still_path + "' /><br> overview:" + episodesforSeason.episodes[i].overview + "<br> air_ date : " + episodesforSeason.episodes[i].air_date + "<br></p>";
        str1 += "<button id='likee' value='" + episodesforSeason.episodes[i].Episode_id + "' onclick='likeEpisode(this.value)'> like  </button>";
        str1 += "<button id='likee' value='" + episodesforSeason.episodes[i].id + "' onclick='favoritepisode(" + episodesforSeason.episodes[i].id + "," + tvId + ")'> add  </button>";
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

function getSTVErrorCB(err) { }
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
    if (user_id_connect != null) {
        favorite = {
            User_id: userconnected.User_id,
            Episode_id: episode_id,
            Show_id: show_id
        }

        let api = "../api/Favorite";
        ajaxCall("POST", api, JSON.stringify(favorite), postFavoriteSuccessCB, postFavoriteErrorCB)
        return false;
    }
    else alert("login");

}

function postTvshowSuccessCB(obj) {

}

function postTvshowErrorCB(err) {

}

function postEpisodeSuccessCB(numInserted) {
}

function postEpisodeErrorCB(err) {
    alert("Gewald");

}

function postFavoriteSuccessCB() {

}

function postFavoriteErrorCB(err) {

}

function likeEpisode(episode_id) {
    let api = "../api/Episode?id=" + episode_id;
    ajaxCall("Get", api, "", findEpisodeSuccessCB, findEpisodeErrorCB)
    return false;
}

function findEpisodeSuccessCB(obj) {
    let api = "../api/Episode?id=" + obj.Episode_id + "&likes=" + obj.Likes;
    ajaxCall("Get", api, "", UpdateEpisodeSuccessCB, UpdateEpisodeErrorCB)
    return false;
}

function UpdateEpisodeSuccessCB() {
}

function UpdateEpisodeErrorCB(err) {

}

function findEpisodeErrorCB() {
}
