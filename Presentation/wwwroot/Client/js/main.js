/* ===========================
   SWEET CAKE PARADISE - JAVASCRIPT
   =========================== */

// ===============================
// DOM READY
// ===============================

document.addEventListener("DOMContentLoaded", function () {
  initializeApp();
});

// ===============================
// INITIALIZATION
// ===============================

function initializeApp() {
  initializeNavbar();
  initializeCart();
  initializeProducts();
  initializeFilter();
  initializeRating();
}

// ===============================
// NAVBAR & NAVIGATION
// ===============================

function initializeNavbar() {
  // Sticky navbar
  window.addEventListener("scroll", function () {
    const navbar = document.querySelector(".navbar");
    if (window.scrollY > 100) {
      navbar.classList.add("shadow");
    } else {
      navbar.classList.remove("shadow");
    }
  });

  // Mobile menu toggle
  const toggler = document.querySelector(".navbar-toggler");
  if (toggler) {
    toggler.addEventListener("click", function () {
      const navbarCollapse = document.querySelector("#navbarNav");
      navbarCollapse.classList.toggle("show");
    });
  }

  // Close navbar when link is clicked
  const navLinks = document.querySelectorAll(".navbar-nav .nav-link");
  navLinks.forEach((link) => {
    link.addEventListener("click", function () {
      const navbarCollapse = document.querySelector("#navbarNav");
      if (navbarCollapse.classList.contains("show")) {
        navbarCollapse.classList.remove("show");
      }
    });
  });
}

// ===============================
// SHOPPING CART
// ===============================

let cart = JSON.parse(localStorage.getItem("cart")) || [];

function addToCart() {
  const productName =
    document.querySelector(".product-title")?.textContent || "Product";
  const productPrice = document.querySelector(".price")?.textContent || "0 VNĐ";
  const quantity = parseInt(document.getElementById("quantity")?.value || 1);

  const cartItem = {
    id: Date.now(),
    name: productName,
    price: productPrice,
    quantity: quantity,
    date: new Date().toLocaleString(),
  };

  cart.push(cartItem);
  localStorage.setItem("cart", JSON.stringify(cart));

  showNotification("Thêm vào giỏ hàng thành công!", "success");
  updateCartCount();
}

function removeFromCart(index) {
  cart.splice(index, 1);
  localStorage.setItem("cart", JSON.stringify(cart));
  updateCartDisplay();
}

function updateCartCount() {
  const cartCount = document.querySelectorAll("[data-cart-count]");
  cartCount.forEach((el) => {
    el.textContent = cart.length;
  });
}

function updateCartDisplay() {
  const cartTable = document.querySelector(".cart-items-table tbody");
  if (cartTable) {
    cartTable.innerHTML = "";
    cart.forEach((item, index) => {
      const row = document.createElement("tr");
      row.innerHTML = `
                <td><a href="#">${item.name}</a></td>
                <td><img src="https://via.placeholder.com/80x80" alt="Product"></td>
                <td>${item.price}</td>
                <td><input type="number" value="${item.quantity}" min="1" class="form-control cart-qty" style="max-width: 70px;"></td>
                <td>${item.price}</td>
                <td><button class="btn btn-sm btn-danger" onclick="removeFromCart(${index})"><i class="fas fa-trash"></i></button></td>
            `;
      cartTable.appendChild(row);
    });
  }
}

// ===============================
// PRODUCTS
// ===============================

function initializeProducts() {
  const wishlistButtons = document.querySelectorAll(".btn-wishlist");
  wishlistButtons.forEach((btn) => {
    btn.addEventListener("click", function (e) {
      e.preventDefault();
      this.classList.toggle("active");
      if (this.classList.contains("active")) {
        this.style.color = "#d4a574";
        showNotification("Đã thêm vào danh sách yêu thích", "info");
      } else {
        this.style.color = "inherit";
        showNotification("Đã xóa khỏi danh sách yêu thích", "info");
      }
    });
  });
}

// ===============================
// FILTERS
// ===============================

function initializeFilter() {
  const filterButtons = document.querySelectorAll(
    ".filter-group .form-check-input",
  );
  filterButtons.forEach((btn) => {
    btn.addEventListener("change", function () {
      applyFilters();
    });
  });

  const priceRange = document.getElementById("priceRange");
  if (priceRange) {
    priceRange.addEventListener("input", function () {
      document.getElementById("maxPrice").textContent =
        this.value.toLocaleString();
    });
  }
}

function applyFilters() {
  console.log("Filters applied");
  // Implement actual filtering logic here
  showNotification("Đã áp dụng bộ lọc", "info");
}

// ===============================
// PRODUCT RATING
// ===============================

function initializeRating() {
  const ratingStars = document.querySelectorAll(".rating-input i");
  ratingStars.forEach((star) => {
    star.addEventListener("click", function () {
      const rating = this.dataset.rating;

      // Update visual
      ratingStars.forEach((s) => {
        if (s.dataset.rating <= rating) {
          s.classList.remove("far");
          s.classList.add("fas");
        } else {
          s.classList.add("far");
          s.classList.remove("fas");
        }
      });
    });
  });
}

// ===============================
// NEWSLETTER
// ===============================

const newsletterForms = document.querySelectorAll(".newsletter-form");
newsletterForms.forEach((form) => {
  form.addEventListener("submit", function (e) {
    e.preventDefault();
    const email = this.querySelector('input[type="email"]').value;

    if (email) {
      showNotification(
        `Đăng ký email ${email} thành công! Nhận mã giảm giá 10% trong email của bạn.`,
        "success",
      );
      this.reset();
    }
  });
});

// ===============================
// CONTACT FORM
// ===============================

const contactForm = document.querySelector(".contact-form");
if (contactForm) {
  contactForm.addEventListener("submit", function (e) {
    e.preventDefault();
    showNotification(
      "Cảm ơn bạn đã liên hệ! Chúng tôi sẽ phản hồi sớm nhất.",
      "success",
    );
    this.reset();
  });
}

// ===============================
// REVIEW FORM
// ===============================

const reviewForm = document.querySelector(".review-form");
if (reviewForm) {
  reviewForm.addEventListener("submit", function (e) {
    e.preventDefault();
    showNotification("Cảm ơn bạn đã để lại đánh giá!", "success");
    this.reset();
  });
}

// ===============================
// COMMENT FORM
// ===============================

const commentFormSubmit = document.querySelector(".comment-form");
if (commentFormSubmit) {
  const submitBtn = commentFormSubmit.querySelector('button[type="submit"]');
  if (submitBtn) {
    submitBtn.addEventListener("click", function (e) {
      e.preventDefault();
      showNotification("Bình luận của bạn đã được gửi!", "success");
      commentFormSubmit.reset();
    });
  }
}

// ===============================
// NOTIFICATIONS
// ===============================

function showNotification(message, type = "info") {
  const alertClass =
    type === "success"
      ? "alert-success"
      : type === "error"
        ? "alert-danger"
        : "alert-info";

  const alertHTML = `
        <div class="alert ${alertClass} alert-dismissible fade show" role="alert" style="position: fixed; top: 80px; right: 20px; z-index: 9999; min-width: 300px;">
            ${message}
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    `;

  const alertElement = document.createElement("div");
  alertElement.innerHTML = alertHTML;
  document.body.appendChild(alertElement.firstElementChild);

  // Auto remove after 4 seconds
  setTimeout(() => {
    const alert = document.querySelector(".alert");
    if (alert) {
      alert.remove();
    }
  }, 4000);
}

// ===============================
// PAGINATION
// ===============================

const paginationLinks = document.querySelectorAll(".pagination a");
paginationLinks.forEach((link) => {
  link.addEventListener("click", function (e) {
    e.preventDefault();
    const pageNum = this.textContent;
    if (pageNum !== "Trước" && pageNum !== "Tiếp theo") {
      window.scrollTo({ top: 0, behavior: "smooth" });
      showNotification(`Đang tải trang ${pageNum}...`, "info");
    }
  });
});

// ===============================
// SMOOTH SCROLL
// ===============================

document.querySelectorAll('a[href^="#"]').forEach((anchor) => {
  anchor.addEventListener("click", function (e) {
    const href = this.getAttribute("href");
    if (href !== "#" && document.querySelector(href)) {
      e.preventDefault();
      const target = document.querySelector(href);
      const headerHeight = document.querySelector(".navbar").offsetHeight;
      const targetPosition =
        target.getBoundingClientRect().top + window.pageYOffset - headerHeight;

      window.scrollTo({
        top: targetPosition,
        behavior: "smooth",
      });
    }
  });
});

// ===============================
// CURRENCY FORMATTER
// ===============================

function formatCurrency(value) {
  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
  }).format(value);
}

// ===============================
// LAZY LOADING IMAGES
// ===============================

if ("IntersectionObserver" in window) {
  const images = document.querySelectorAll("img[data-src]");
  const imageObserver = new IntersectionObserver((entries, observer) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        const img = entry.target;
        img.src = img.dataset.src;
        img.removeAttribute("data-src");
        observer.unobserve(img);
      }
    });
  });

  images.forEach((img) => imageObserver.observe(img));
}

// ===============================
// CHECKOUT FORM
// ===============================

const checkoutForm = document.querySelector(".checkout-form");
if (checkoutForm) {
  const submitBtn = checkoutForm.querySelector('button[type="submit"]');
  if (submitBtn) {
    submitBtn.addEventListener("click", function (e) {
      e.preventDefault();

      const name = document.querySelector('input[placeholder*="Họ"]');
      const email = document.querySelector('input[placeholder*="Email"]');
      const phone = document.querySelector(
        'input[placeholder*="Số điện thoại"]',
      );

      if (name && email && phone && name.value && email.value && phone.value) {
        showNotification(
          "Đơn hàng của bạn đã được tiếp nhận! Chúng tôi sẽ xác nhận qua email.",
          "success",
        );
        setTimeout(() => {
          window.location.href = "index.html";
        }, 2000);
      } else {
        showNotification("Vui lòng điền đầy đủ thông tin bắt buộc", "error");
      }
    });
  }
}

// ===============================
// SEARCH FUNCTIONALITY
// ===============================

const searchForms = document.querySelectorAll(
  ".blog-widget:first-of-type form",
);
searchForms.forEach((form) => {
  const searchBtn = form.querySelector("button");
  if (searchBtn) {
    searchBtn.addEventListener("click", function (e) {
      e.preventDefault();
      const keyword = form.querySelector("input").value;
      if (keyword) {
        showNotification(`Tìm kiếm cho: "${keyword}"`, "info");
        // Implement actual search logic
      }
    });
  }
});

// ===============================
// DYNAMIC PRICING (FOR CHECKOUT)
// ===============================

const shippingRadios = document.querySelectorAll('input[name="shipping"]');
shippingRadios.forEach((radio) => {
  radio.addEventListener("change", function () {
    const shippingFee = this.nextElementSibling.querySelector(".shipping-fee");
    if (shippingFee) {
      const fee = shippingFee.textContent;
      const shippingFeeDisplay = document.getElementById("shipping-fee");
      if (shippingFeeDisplay) {
        shippingFeeDisplay.textContent = fee;
      }
    }
  });
});

// ===============================
// UTILITY FUNCTIONS
// ===============================

// Get query parameters
function getQueryParam(param) {
  const urlParams = new URLSearchParams(window.location.search);
  return urlParams.get(param);
}

// Update product filters based on URL
function filterByCategory() {
  const category = getQueryParam("category");
  if (category) {
    showNotification(`Hiển thị sản phẩm: ${category}`, "info");
  }
}

filterByCategory();

// ===============================
// PERFORMANCE OPTIMIZATIONS
// ===============================

// Debounce function for resize events
function debounce(func, wait) {
  let timeout;
  return function executedFunction(...args) {
    const later = () => {
      clearTimeout(timeout);
      func(...args);
    };
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
  };
}

// Handle window resize
window.addEventListener(
  "resize",
  debounce(function () {
    console.log("Window resized");
  }, 250),
);

// ===============================
// PRINT FUNCTION
// ===============================

function printOrder() {
  window.print();
  return false;
}

// ===============================
// EXPORT/DOWNLOAD CSV
// ===============================

function exportCartToCSV() {
  if (cart.length === 0) {
    showNotification("Giỏ hàng của bạn trống!", "error");
    return;
  }

  let csv = "Tên sản phẩm,Giá,Số lượng,Ngày thêm\n";
  cart.forEach((item) => {
    csv += `"${item.name}","${item.price}",${item.quantity},"${item.date}"\n`;
  });

  const element = document.createElement("a");
  element.setAttribute(
    "href",
    "data:text/csv;charset=utf-8," + encodeURIComponent(csv),
  );
  element.setAttribute("download", "dong_hang.csv");
  element.style.display = "none";
  document.body.appendChild(element);
  element.click();
  document.body.removeChild(element);

  showNotification("Đã tải xuống danh sách sản phẩm", "success");
}

console.log("Sweet Cake Paradise - JavaScript initialized successfully!");
