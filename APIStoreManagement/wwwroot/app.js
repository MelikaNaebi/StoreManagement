namespace APIStoreManagement.wwwroot
{
    const baseApiUrl = "https://localhost:7043/api";

    // تابعی برای دریافت لباس‌ها
    async function fetchClothings() {
        try {
            const response = await fetch(`${baseApiUrl}/clothings`);
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            const data = await response.json();
            console.log(data);
        } catch (error) {
            console.error('Error fetching clothings:', error);
        }
    }

    // تابعی برای ایجاد یک لباس جدید
    async function createClothing(clothing) {
        try {
            const response = await fetch(`${baseApiUrl}/clothings`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(clothing),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }

            const newClothing = await response.json();
            console.log('New clothing added:', newClothing);
        } catch (error) {
            console.error('Error creating clothing:', error);
        }
    }

    // تابعی برای دریافت هزینه‌ها
    async function fetchCosts() {
        try {
            const response = await fetch(`${baseApiUrl}/costs`);
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            const data = await response.json();
            console.log(data);
        } catch (error) {
            console.error('Error fetching costs:', error);
        }
    }

    // تابعی برای ایجاد هزینه جدید
    async function createCost(cost) {
        try {
            const response = await fetch(`${baseApiUrl}/costs`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(cost),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }

            const newCost = await response.json();
            console.log('New cost added:', newCost);
        } catch (error) {
            console.error('Error creating cost:', error);
        }
    }

    // تابعی برای دریافت موجودی‌ها
    async function fetchInventories() {
        try {
            const response = await fetch(`${baseApiUrl}/inventories`);
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            const data = await response.json();
            console.log(data);
        } catch (error) {
            console.error('Error fetching inventories:', error);
        }
    }

    // تابعی برای ایجاد موجودی جدید
    async function createInventory(inventory) {
        try {
            const response = await fetch(`${baseApiUrl}/inventories`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(inventory),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }

            const newInventory = await response.json();
            console.log('New inventory added:', newInventory);
        } catch (error) {
            console.error('Error creating inventory:', error);
        }
    }

    // تابعی برای دریافت سفارش‌ها
    async function fetchOrders() {
        try {
            const response = await fetch(`${baseApiUrl}/orders`);
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            const data = await response.json();
            console.log(data);
        } catch (error) {
            console.error('Error fetching orders:', error);
        }
    }

    // تابعی برای ایجاد سفارش جدید
    async function createOrder(order) {
        try {
            const response = await fetch(`${baseApiUrl}/orders`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(order),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }

            const newOrder = await response.json();
            console.log('New order added:', newOrder);
        } catch (error) {
            console.error('Error creating order:', error);
        }
    }

    // تابعی برای دریافت الگوها
    async function fetchPatterns() {
        try {
            const response = await fetch(`${baseApiUrl}/patterns`);
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            const data = await response.json();
            console.log(data);
        } catch (error) {
            console.error('Error fetching patterns:', error);
        }
    }

    // تابعی برای ایجاد الگوی جدید
    async function createPattern(pattern) {
        try {
            const response = await fetch(`${baseApiUrl}/patterns`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(pattern),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }

            const newPattern = await response.json();
            console.log('New pattern added:', newPattern);
        } catch (error) {
            console.error('Error creating pattern:', error);
        }
    }

    // تابعی برای دریافت فروش‌ها
    async function fetchSales() {
        try {
            const response = await fetch(`${baseApiUrl}/sales`);
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            const data = await response.json();
            console.log(data);
        } catch (error) {
            console.error('Error fetching sales:', error);
        }
    }

    // تابعی برای ایجاد فروش جدید
    async function createSale(sale) {
        try {
            const response = await fetch(`${baseApiUrl}/sales`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(sale),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }

            const newSale = await response.json();
            console.log('New sale added:', newSale);
        } catch (error) {
            console.error('Error creating sale:', error);
        }
    }

    // تابعی برای دریافت اندازه‌ها
    async function fetchSizes() {
        try {
            const response = await fetch(`${baseApiUrl}/sizes`);
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            const data = await response.json();
            console.log(data);
        } catch (error) {
            console.error('Error fetching sizes:', error);
        }
    }

    // تابعی برای ایجاد اندازه جدید
    async function createSize(size) {
        try {
            const response = await fetch(`${baseApiUrl}/sizes`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(size),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }

            const newSize = await response.json();
            console.log('New size added:', newSize);
        } catch (error) {
            console.error('Error creating size:', error);
        }
    }

    // نمونه‌هایی از داده‌ها برای ایجاد موارد جدید
    const newClothing = {
        name: "T-Shirt",
        patternId: 1,
        sizeId: 2,
    };

    const newCost = {
        amount: 20,
    };

    const newInventory = {
        clothingId: 1,
        quantity: 100,
    };

    const newOrder = {
        sizeId: 2,
        patternId: 1,
    };

    const newPattern = {
        name: "Striped",
    };

    const newSale = {
        clothingId: 1,
        quantity: 2,
    };

    const newSize = {
        name: "L",
    };

    // فراخوانی تابع‌ها
    fetchClothings();
    createClothing(newClothing);

    fetchCosts();
    createCost(newCost);

    fetchInventories();
    createInventory(newInventory);

    fetchOrders();
    createOrder(newOrder);

    fetchPatterns();
    createPattern(newPattern);

    fetchSales();
    createSale(newSale);

    fetchSizes();
    createSize(newSize);


}
