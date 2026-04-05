/* ============================================
   ADMIN LAYOUT - JAVASCRIPT
   ============================================ */

document.addEventListener("DOMContentLoaded", function () {
  // === SIDEBAR TOGGLE ===
  const menuToggle = document.getElementById("menuToggle");
  const sidebarToggle = document.getElementById("sidebarToggle");
  const sidebar = document.getElementById("sidebar");

  if (menuToggle) {
    menuToggle.addEventListener("click", function () {
      sidebar.classList.toggle("collapsed");
    });
  }

  if (sidebarToggle) {
    sidebarToggle.addEventListener("click", function () {
      sidebar.classList.remove("collapsed");
    });
  }

  // === ACTIVE NAV LINK ===
  const currentPath = window.location.pathname;
  const navLinks = document.querySelectorAll(".sidebar-nav .nav-link");

  navLinks.forEach((link) => {
    if (
      link.getAttribute("href") === currentPath ||
      currentPath.includes(link.getAttribute("asp-controller"))
    ) {
      link.classList.add("active");
    }
  });

  // === CLOSE SIDEBAR ON MOBILE WHEN CLICKING LINK ===
  if (window.innerWidth <= 992) {
    navLinks.forEach((link) => {
      link.addEventListener("click", function () {
        sidebar.classList.add("collapsed");
      });
    });
  }

  // === RESPONSIVE SIDEBAR ===
  window.addEventListener("resize", function () {
    if (window.innerWidth > 992) {
      sidebar.classList.remove("collapsed");
    }
  });

  // === BODY CLASS FOR ADMIN LAYOUT ===
  document.body.classList.add("admin");
});

// === TOAST NOTIFICATIONS ===
function showToast(message, type = "info", duration = 3000) {
  const toastId = "toast-" + Date.now();
  const toastHTML = `
    <div id="${toastId}" class="toast-notification toast-${type}">
      <div class="toast-content">
        <i class="fa-solid fa-${getIconByType(type)}"></i>
        <p>${message}</p>
      </div>
      <button class="toast-close" onclick="closeToast('${toastId}')">&times;</button>
    </div>
  `;

  const container =
    document.querySelector(".toast-container") || createToastContainer();
  container.insertAdjacentHTML("beforeend", toastHTML);

  const toast = document.getElementById(toastId);
  setTimeout(() => {
    toast.classList.add("show");
  }, 10);

  if (duration > 0) {
    setTimeout(() => {
      closeToast(toastId);
    }, duration);
  }
}

function closeToast(toastId) {
  const toast = document.getElementById(toastId);
  if (toast) {
    toast.classList.remove("show");
    setTimeout(() => {
      toast.remove();
    }, 300);
  }
}

function getIconByType(type) {
  const icons = {
    success: "check-circle",
    error: "exclamation-circle",
    warning: "exclamation-triangle",
    info: "info-circle",
  };
  return icons[type] || icons["info"];
}

function createToastContainer() {
  const container = document.createElement("div");
  container.className = "toast-container";
  document.body.appendChild(container);
  return container;
}

// === CONFIRMATION DIALOG ===
function confirmDelete(message = "Bạn có chắc muốn xóa?") {
  return confirm(message);
}

// === SEARCH & FILTER ===
function filterTable(tableId, searchInputId) {
  const searchInput = document.getElementById(searchInputId);
  const table = document.getElementById(tableId);

  if (searchInput && table) {
    searchInput.addEventListener("keyup", function () {
      const filter = searchInput.value.toLowerCase();
      const rows = table
        .getElementsByTagName("tbody")[0]
        .getElementsByTagName("tr");

      Array.from(rows).forEach((row) => {
        const text = row.textContent.toLowerCase();
        row.style.display = text.includes(filter) ? "" : "none";
      });
    });
  }
}

// === DATE FORMATTING ===
function formatDate(date) {
  if (typeof date === "string") {
    date = new Date(date);
  }
  return date.toLocaleDateString("vi-VN", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
  });
}

// === CURRENCY FORMATTING ===
function formatCurrency(value) {
  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
  }).format(value);
}

// === CSV EXPORT ===
function exportTableToCSV(tableId, filename) {
  const table = document.getElementById(tableId);
  let csv = [];

  const rows = table.querySelectorAll("tr");
  rows.forEach((row) => {
    const cells = row.querySelectorAll("td, th");
    const rowData = [];
    cells.forEach((cell) => {
      rowData.push('"' + cell.textContent + '"');
    });
    csv.push(rowData.join(","));
  });

  downloadCSV(csv.join("\n"), filename);
}

function downloadCSV(csv, filename) {
  const csvFile = new Blob([csv], { type: "text/csv" });
  const downloadLink = document.createElement("a");
  downloadLink.href = URL.createObjectURL(csvFile);
  downloadLink.download = filename || "export.csv";
  document.body.appendChild(downloadLink);
  downloadLink.click();
  document.body.removeChild(downloadLink);
}

// === BATCH ACTIONS ===
function setupBatchActions(tableId, actionButtonId) {
  const checkboxes = document.querySelectorAll(
    `#${tableId} tbody input[type="checkbox"]`,
  );
  const actionButton = document.getElementById(actionButtonId);
  const selectAllCheckbox = document.querySelector(
    `#${tableId} thead input[type="checkbox"]`,
  );

  if (selectAllCheckbox) {
    selectAllCheckbox.addEventListener("change", function () {
      checkboxes.forEach((checkbox) => {
        checkbox.checked = this.checked;
      });
      updateBatchActionButton();
    });
  }

  checkboxes.forEach((checkbox) => {
    checkbox.addEventListener("change", updateBatchActionButton);
  });

  function updateBatchActionButton() {
    const checkedCount = document.querySelectorAll(
      `#${tableId} tbody input[type="checkbox"]:checked`,
    ).length;
    if (actionButton) {
      actionButton.style.display = checkedCount > 0 ? "block" : "none";
    }
  }
}

// === MODAL HELPERS ===
function showModal(modalId) {
  const modal = new bootstrap.Modal(document.getElementById(modalId));
  modal.show();
}

function hideModal(modalId) {
  const modal = bootstrap.Modal.getInstance(document.getElementById(modalId));
  if (modal) modal.hide();
}

// === LOADING STATE ===
function setLoading(isLoading, element) {
  if (isLoading) {
    element.disabled = true;
    element.innerHTML =
      '<span class="spinner-border spinner-border-sm me-2"></span>Đang xử lý...';
  } else {
    element.disabled = false;
    element.innerHTML = element.dataset.originalText || "Submit";
  }
}
