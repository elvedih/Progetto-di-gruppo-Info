document.addEventListener("DOMContentLoaded", () => {
    checkAuth();
})

function checkAuth() {
    const token = localStorage.getItem("token");

    if (!token) {
        if (window.location.pathname.includes("/home.html")) {
            window.location.href = "/login.html";
        }
        return false;
    }

    if (token && window.location.pathname.includes("/login.html")) {
        window.location.href = "/home.html";
    }

    if (token && window.location.pathname.includes("/signup.html")) {
        window.location.href = "/home.html"
    }

    return true;
}

async function signup() {
    event.preventDefault();
    const username = document.getElementById("su_username").value;
    const password = document.getElementById("su_password").value;

    const response = await fetch("https://localhost:7006/api/auth/signup", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ username, password })
    });

    if (response.ok) {
        alert("Account creato! Ora puoi accedere.");
        window.location.href = "login.html";
    } else {
        const msg = await response.text();
        document.getElementById("error").innerText = msg;
    }
}

async function login() {
    event.preventDefault();
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    const response = await fetch("https://localhost:7006/api/auth/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ username, password })
    });

    if (response.ok) {
        const data = await response.json();
        localStorage.setItem("token", data.token);
        window.location.href = "home.html";
    } else {
        document.getElementById("error").innerText = "Login non valido";
    }
}

async function deleteAccount() {
    const confirmDelete = confirm("Sei sicuro di voler eliminare il tuo account?");

    if (!confirmDelete) return;

    const token = localStorage.getItem("token");

    const response = await fetch("https://localhost:7006/api/auth/delete-account", {
        method: "POST",
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });

    if (response.ok) {
        alert("Account eliminato con successo");
        localStorage.removeItem("token");
        window.location.href = "/login.html";
    } else {
        const msg = await response.text();
        alert("Errore: " + msg);
    }
}


async function downloadGame() {
    event.preventDefault();
    const token = localStorage.getItem("token");

    const response = await fetch("https://localhost:7006/api/game/download", {
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });

    if (!response.ok) {
        alert("Errore: non autorizzato");
        return;
    }

    const blob = await response.blob();
    const url = window.URL.createObjectURL(blob);

    const a = document.createElement("a");
    a.href = url;
    a.download = "Requiem.zip";
    document.body.appendChild(a);
    a.click();
    a.remove();
}