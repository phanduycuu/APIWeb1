const form = document.querySelector("form");
const curentPass = document.querySelector("#currentpass");
const newPass = document.querySelector("#newpass");
const repass = document.querySelector("#repass");

function change() {
    // Xóa các thông báo lỗi trước đó
    const errors = form.querySelectorAll(".text-danger");
    errors.forEach((error) => (error.innerText = ""));

    let isValid = true;
    alert(newPass);
    // Kiểm tra CurrentPass không rỗng
    if (!curentPass.value) {
        document.querySelector("#errorcurrentpass").innerText = "Current password is required.";
        isValid = false;
    }
    else {
        document.querySelector("#errorcurrentpass").innerText = "";
    }
    
    // Kiểm tra NewPass không rỗng
    if (!newPass.value) {
        document.querySelector("#errornewpass").innerText = "New password is required.";
        isValid = false;
    }
    

    if (!repass.value.trim()) {
        document.querySelector("#errorrepass").innerText = "Re password is required.";
        isValid = false;
    }
    else {
        document.querySelector("#errorrepass").innerText = "";
    }

    // Kiểm tra độ dài của NewPass
    if (newPass.value.length < 6) {
        document.querySelector("#errornewpass").innerText = "New password must be at least 6 characters.";
        isValid = false;
    }
    else {
        document.querySelector("#errornewpass").innerText = "";
    }

    // Kiểm tra NewPass có ký tự viết hoa và viết thường
    const hasUpperCase = /[A-Z]/.test(newPass.value);
    const hasLowerCase = /[a-z]/.test(newPass.value);

    if (!hasUpperCase || !hasLowerCase) {
        document.querySelector("#errornewpass").innerText = "New password must contain both uppercase and lowercase letters.";
        isValid = false;
    }
    else {
        document.querySelector("#errornewpass").innerText = "";
    }

    // Kiểm tra RePass khớp với NewPass
    if (newPass.value !== repass.value) {
        document.querySelector("#errornewpass").innerText = "New password and confirmation must match.";
        isValid = false;
    }
    else {
        document.querySelector("#errornewpass").innerText = "";
    }

    // Nếu không hợp lệ, ngăn không cho submit form
    if (!isValid) {
        event.preventDefault();
    } else {
        form.submit();
    }
}