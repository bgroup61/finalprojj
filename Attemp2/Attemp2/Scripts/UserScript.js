
$(document).ready(function () {
    user = JSON.parse(localStorage["User"]);
    happybirthday()
    let api = "../api/User?email=" + user.Email + "&password=" + user.Password;
    ajaxCall("GET", api, "", getuserSuccessCB, getuserErrorCB)
    return false;
});

function getuserSuccessCB(obj) {
    let api = "../api/Favorite?id=" + obj.User_id;
    ajaxCall("GET", api, "", getfavoriteSuccessCB, getfavoriteErrorCB)
    return false;
}

function getuserErrorCB(err) {

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

function getfavoriteSuccessCB(fav) {
    let api = "../api/Episode?user_id=" + fav[0].User_id + "&episode_id=" + fav[0].Episode_id;
    ajaxCall("GET", api, "", getepisodeuserSuccessCB, getepisodeuserErrorCB)
    return false;

}

function getepisodeuserSuccessCB(episodelist) {
    tableStr = "<table align:center;><tr><th>Eposide_name</th><th>Season</th><th>First_air_date</th><th>Description</th></tr>"
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
    $("#tablestr").html(tableStr);
}

function getepisodeuserErrorCB(err) {

}

function getErrorCB(err) {
    console.log(err);
}


function getfavoriteErrorCB(err) {
    alert("err")
}

