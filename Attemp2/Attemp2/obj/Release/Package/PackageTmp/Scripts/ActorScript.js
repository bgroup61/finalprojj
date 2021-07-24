
    user = null;
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
            }
            //get actor:
            if (localStorage["ActorId"] != undefined) {
        actorId = localStorage["ActorId"];
                //https://api.themoviedb.org/3/person/49001?api_key=0c4c09dc31abca426ccfff55b6d426c3
                actorurl = url + "/person/" + actorId + "?" + api_key;
                ajaxCall("GET", actorurl, " ", getActorSuccessCB, getActorErrorCB);
                return false;
            }

        });

        function logout() {
        localStorage.clear();
            location.reload();
            window.location.replace("../Pages/HomePage.html");
        }

        function getActorSuccessCB(actorInfo) {

        let poster = imagePath + actorInfo.profile_path;
            str = `<br>
        <div id='moreinfo' class='row' >
            <div class='column'>
                <img src="${poster}" />
            </div>
            <div class='column'>
                <p>${actorInfo.name}</p>
                <p>${actorInfo.also_known_as[0]}</p>
                <p>${actorInfo.biography}</p>
                <p>${actorInfo.birthday}</p>
                <p>${actorInfo.known_for_department}</p>
                <p>${actorInfo.place_of_birth}</p>
            </div>
        </div > `;
            $("#actorinfo").html(str);

            //https://api.themoviedb.org/3/person/49001/tv_credits?api_key=0c4c09dc31abca426ccfff55b6d426c3
            actortvurl = url + "/person/" + actorId + "/tv_credits?" + api_key;
            ajaxCall("GET", actortvurl, " ", getActortv_creditsSuccessCB, getActortv_creditsErrorCB);
            return false;
        }

        function getActortv_creditsSuccessCB(tv_credits) {
            tvarray = [];
            for (var i = 0; i < tv_credits.cast.length;i++) {
            tvOfActor = {
                Name: tv_credits.cast[i].name,
                Date: tv_credits.cast[i].first_air_date,
                Character: tv_credits.cast[i].character,
                Count: tv_credits.cast[i].episode_count
            }
                tvarray.push(tvOfActor);
            }

            tvarray.sort(function (a, b) {
                var c = new Date(a.Date);
                var d = new Date(b.Date);
                return d-c;
            });

            tableStr = `<table> <tr>
                        <th>TV_Show_name</th >
                        <th>Charecter_name</th>
                        <th>TV_first_air_date</th>
                        <th>Episodes_number</th>
                        </tr >`
            for (var j = 0; j < tvarray.length;j++) {
                tableStr += `
                               <tr>
                               <td>${tvarray[j].Name}_</td>
                               <td> as ${tvarray[j].Character}_</td>
                               <td>${tvarray[j].Date}_</td>
                               <td> (episodes count ${tvarray[j].Count})</td>
                               </tr>`
            }
            tableStr += "</table>";
            $("#tablestr").html(tableStr);
        }

        function getActortv_creditsErrorCB(error) {

        }
        function getActorErrorCB(error) {

        }

    