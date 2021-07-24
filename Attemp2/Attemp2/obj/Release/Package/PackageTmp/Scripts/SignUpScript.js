   user = null;
        $(document).ready(function () {


        $("#formid").submit(function () {
            user = {
                Name: $("#name").val(),
                LastName: $("#lname").val(),
                Email: $("#email").val(),
                Birthday: $("#birthday").val(),
                Password: $("#psw").val(),
                Telephone: $("#tel").val(),
                Gender: $("input[type=radio]:checked").val(),
                Category: $("#Category").val(),
                Role: "user"
            }


            let api = "../api/User";
            ajaxCall("Post", api, JSON.stringify(user), postUserSuccessCB, postUserErrorCB)
            return false;
        });
        });

        function postUserSuccessCB() {
        localStorage["User"] = JSON.stringify(user);
            document.getElementById('formid').reset();
            window.location.replace("../Pages/HomePage.html");
        }

        function postUserErrorCB(err) {
        document.getElementById('formid').reset();
            $("#errlabel").html("Email already exists,enter another email address");
        }

