
    str_codeinvalid = "";
    count = 1;
    usercode = null;
        $(document).ready(function () {
        $("#formid1").submit(function () {

            email = $("#emaill").val();
            password = $("#password").val();

            let api = "../api/User?email=" + email + "&password=" + password;
            ajaxCall("GET", api, "", getuserSuccessCB, getuserErrorCB)
            return false;
        });
        });

        function getuserSuccessCB(user) {
        usercode = user;
            if (user != null) {
        code = Math.floor(Math.random() * 9999);
                var ms = "Code :" + code;
                login = {
        massage: ms,
                    phone: user.Telephone
                }

                localStorage["User"] = JSON.stringify(user);
                let api = "../api/SendM"
                ajaxCall("post", api, JSON.stringify(login), sendSuccessCB, sendErrorCB)
                return false;
            }
            else {
        $("#getuser").html("user is not found,try again");
            }
        }

        function getuserErrorCB(error) {

    }

        function sendSuccessCB() {
        document.getElementById('formid1').reset();
            document.getElementById('formdiv').style.display = "none";
            document.getElementById('validiv').style.display = "block";
            str = "<input type='text' id='code' placeholder='enter the code' required>";
            str += "<button id='codevalid' onclick='validCode()'>OK</button>";
            document.getElementById('validiv').innerHTML = str + str_codeinvalid;
        }

        function sendErrorCB() {

        }

        function validCode() {
            ucode = $("#code").val();
            if (code == ucode) {
            document.getElementById('formdiv').style.display = "block";
                document.getElementById('validiv').style.display = "none";
                window.location.replace("../Pages/HomePage.html");
            }
            else if (count <= 2) {
            count++;
                str_codeinvalid = "<br><p style='color:blue'>Invalid code</p>";
            }
            else {
                localStorage["User"] = null;
                window.location.replace("../Pages/HomePage.html");
            }
           
        }
    