$(document).ready(function () {
    user = JSON.parse(localStorage["User"]);
    if (user != null) {
        happybirthday();
        getUsers();
        getTVShows();
        getEpisodes();
    }
});

function logout() {
    localStorage.clear();
    location.reload();
    window.location.replace("../Pages/HomePage.html");
}

function happybirthday() {
    // name, string lastName, string email, string birthday, string password, string telephone, string gender, string category, int user_id)

    userstr = "<p> Wellcome " + user.Name + "  " + user.LastName + "</p>";
    var myDate = new Date(user.Birthday);
    var userdate = myDate.getFullYear() + '-' + (myDate.getMonth() + 1) + '-' + myDate.getDate();
    var today = new Date();
    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    if (date == userdate) {
        userstr += "<p style='background_color:white'>Happy Birthday<i class='fa fa-birthday-cake'></i></p>";
    }
    $("#userPH").html(userstr);

}

function getUsers() {

    let api = "../api/User"
    ajaxCall("GET", api, "", getUsersSuccessCB, getUsersErrorCB)
    return false;
}

function getUsersSuccessCB(userslist) {
    tableStr = "<div>Users Table</div><br><table><tr><th>ID</th><th>Name</th><th>Last_Name</th><th>Email</th><th>Birthday</th><th>PhoneN</th><th>Gender</th><th>Category</th><th>Role</th></tr>";
    for (var j = 0; j < userslist.length; j++) {
        tableStr += `
                               <tr>
                               <td>${userslist[j].User_id}</td>
                               <td>${userslist[j].Name}_</td>
                               <td> Season : ${userslist[j].LastName}</td>
                               <td>${userslist[j].Email}</td>
                               <td>${userslist[j].Birthday}</td>
                               <td>${userslist[j].Telephone}</td>
                               <td>${userslist[j].Gender}</td>
                               <td>${userslist[j].Category}</td>
                               <td>${userslist[j].Role}</td>

                               </tr>`
    }
    tableStr += "</table>";
    $("#usersPH").html(tableStr);
}

function getUsersErrorCB(err) {

}


function getTVShows() {
    let api = "../api/TvShow";
    ajaxCall("GET", api, "", getTVSuccessCB, getTVErrorCB)
    return false;
}

function getTVSuccessCB(tvList) {
    tableStr = "<div>TV_Show Table</div><br><table><tr><th>ID</th><th>Name</th><th>FirstAirDate</th><th>Country</th><th>Language</th><th>Overview</th><th>Popularity</th><th>Likes</th></tr>";
    for (var j = 0; j < tvList.length; j++) {
        tableStr += `
                               <tr>
                               <td>${tvList[j].Show_id}</td>
                               <td>${tvList[j].Show_name}_</td>
                               <td>${tvList[j].First_air_date}</td>
                               <td>${tvList[j].Origin_country}</td>
                               <td>${tvList[j].Original_language}</td>
                               <td>${tvList[j].Overview}</td>
                               <td>${tvList[j].Popularity}</td>
                               <td>${tvList[j].Likes}</td>                           
                               </tr>`
    }
    tableStr += "</table>";
    $("#tvPH").html(tableStr);
}

function getTVErrorCB(err) {

}
function getEpisodes() {
    let api = "../api/Episode";
    ajaxCall("GET", api, "", getEPSuccessCB, getEPErrorCB)
    return false;
}

function getEPSuccessCB(episodelist) {
    tableStr = "<div>Episodes Table</div><br><table><tr><th>Name</th><th>Season</th><th>Date</th><th>Overview</th></tr>";
    for (var j = 0; j < episodelist.length; j++) {
        tableStr += `
                               <tr>
                               <td>${episodelist[j].Episode_name}_</td>
                               <td> Season : ${episodelist[j].Season_num}</td>
                               <td>${episodelist[j].Date}</td>
                               <td>${episodelist[j].Description}</td>
                               </tr>`
    }
    tableStr += "</table>";
    $("#episodePH").html(tableStr);
}

function getEPErrorCB(err) {

}

function getErrorCB(err) {
    console.log(err);
}

