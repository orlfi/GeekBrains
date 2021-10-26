'use strict';

let fitlerPopup = document.querySelector('.filterPopup');
let fitlerLabel = document.querySelector('.filterLabel');
let filterIcon = document.querySelector('.filterIcon');

fitlerLabel.addEventListener('click', function() {
    fitlerPopup.classList.toggle('hidden');
    fitlerLabel.classList.toggle('filterLabelPink');
    filterIcon.classList.toggle('filterIconPink');

    if (filterIcon.getAttribute('src') === 'images/filter.svg') {
        filterIcon.setAttribute('src', 'images/filterHover.svg')
    } else {
        filterIcon.setAttribute('src', 'images/filter.svg')
    }
});

let filterHeaders = document.querySelectorAll('.filterCategoryHeader');
filterHeaders.forEach(function(header) {
    header.addEventListener('click', function(event) {
        event.target.nextElementSibling.classList.toggle('hidden');
    })
});

let filterSizes = document.querySelector('.filterSizes');
let filterSizeWrap = document.querySelector('.filterSizeWrap');
filterSizeWrap.addEventListener('click', function() {
    filterSizes.classList.toggle('hidden');
});

const products = [
    new Product(1, "Крутая курточка", 50, "1.jpg","Known for her sculptural takes on traditional tailoring, Australian arbiter of cool Kym Ellery teams up with Moda Operandi."),
    new Product(2, "Зажигай подруга", 45, "2.jpg","Known for her sculptural takes on traditional tailoring, Australian arbiter of cool Kym Ellery teams up with Moda Operandi."),
    new Product(3, "Штанишки для мальчишки", 65, "3.jpg","Known for her sculptural takes on traditional tailoring, Australian arbiter of cool Kym Ellery teams up with Moda Operandi."),
    new Product(4, "Бородатый стиляга", 30, "4.jpg","Known for her sculptural takes on traditional tailoring, Australian arbiter of cool Kym Ellery teams up with Moda Operandi."),
    new Product(5, "Красава пиджачок", 70, "5.jpg","Known for her sculptural takes on traditional tailoring, Australian arbiter of cool Kym Ellery teams up with Moda Operandi."),
    new Product(6, "Стильная рубашонка", 35, "6.jpg","Known for her sculptural takes on traditional tailoring, Australian arbiter of cool Kym Ellery teams up with Moda Operandi."),
];

const featuredItemsEl = document.querySelector(".featuredItems");

// Fills in the list of products
products.forEach(product => {
    featuredItemsEl.insertAdjacentHTML("beforeend",
        `<div class="featuredItem">

            <div class="featuredImgWrap">
                <img src="images/featured/${product.image}" alt="">
                <div class="featuredImgDark">
                    <button>
                        <img src="images/cart.svg" alt="">
                        Add to Cart
                    </button>
                </div>
            </div>

            <div class="featuredData" data-id="${product.id}" data-name="${product.name}" data-price="${product.price}">
                <div class="featuredName">
                    ${product.name}
                </div>
                <div class="featuredText">
                    ${product.text}
                </div>
                <div class="featuredPrice">
                    $${product.price.toFixed(2)}
                </div>
            </div>

        </div>`
    );
})