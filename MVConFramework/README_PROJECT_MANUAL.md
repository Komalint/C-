# FoodExpress - Complete Food Delivery & Multi-Store Management Platform

An end-to-end ASP.NET MVC web application connecting Customers, Restaurant Partners, Delivery Agents, and System Administrators with Microsoft SQL Server.

---

## Key Features & Role Breakdown

### 1. Customer Experience (`/Customer`)
- **Multi-City Restaurant Discovery**: Browse verified food partners across Delhi, Mumbai, Pune, Jaipur, and Lucknow, filtered by City and Cuisine.
- **Digital Interactive Menu**: Filter items by Category (Starters, Main Course, Biryani, Breads, Desserts, Beverages) and Veg / Non-Veg tags.
- **Dynamic Cart Drawer**: Live subtotal, 5% GST, ₹40 delivery fee (free over ₹500), and auto-calculated kitchen preparation time.
- **Secure Dummy Payment Gateway**:
  - Instant UPI (Google Pay, PhonePe, Paytm, BHIM, custom VPA).
  - Dynamic UPI QR Code (scan-to-pay with live merchant data).
  - Credit/Debit Card simulator with CVV & 3D Secure verification.
  - Cash on Delivery (COD).
- **Dynamic Receipt & Invoice (`/Customer/Receipt/{id}`)**:
  - Dynamically generated with Restaurant Name, FSSAI Lic. No, Customer Details, itemized breakdown, transparent 7% platform fee display, and one-click Print / PDF saving.
- **Kitchen Preparation Allotment & Live Tracking (`/Customer/TrackOrder/{id}`)**:
  - Preparation time dynamically allotted based on order size:
    - **≤ 2 items**: 15 minutes
    - **3 to 5 items**: 30 minutes
    - **> 10 items**: 60 minutes (1 hour)
  - Live countdown timer for kitchen packing.
  - As soon as food is packed, an active delivery agent is **automatically assigned from the database**.
  - **45-Minute Delivery Window**: Interactive route map visualization showing Restaurant origin pin, Delivery Rider in-transit marker, Customer Home pin, and real-time distance in kilometers.
  - Interactive "Simulate Next Stage" button for live presentation/demo.
- **Order History & Review System (`/Customer/MyOrders`)**:
  - Track all previous orders.
  - Rate delivered orders (1 to 5 stars) with comments, updating the restaurant's rating in real-time.

---

### 2. Restaurant Partner Kitchen Portal (`/Restaurant`)
- **Kitchen Dashboard**: Manage live orders, view preparation timers, gross sales, 7% platform fee deduction, and net payout.
- **Menu Management (Full CRUD) (`/Restaurant/ManageMenu`)**:
  - Create new dishes with category, price, veg/non-veg toggle, prep time, and image URL.
  - Edit existing dishes.
  - Delete or toggle availability of menu items.
- **Invoices & Sales Register (`/Restaurant/Invoices`)**:
  - Itemized financial statement of all orders.
  - 7% platform fee deduction calculated on gross sales.
  - 93% net restaurant payout.
- **Reviews & Feedback Evaluation (`/Restaurant/Reviews`)**:
  - View all customer ratings and comments.
  - "Evaluate & Respond" modal allowing kitchen owners to post official responses.
- **Certificate of Trust System (`/Restaurant/Certificate`)**:
  - Restaurants that complete **200+ orders with an average rating of 3.5 to 5.0 stars** unlock the official **Certificate of Customer Trust & Hygiene**.
  - Gold-embossed printable certificate featuring Restaurant Name, Owner, FSSAI Lic. No, Hygiene Score (e.g. 95/100), Official Seal, and Auditor Signatures.

---

### 3. Admin Portal (`/AdminFood`)
- **City-Wise Analytics & Sales**:
  - Filter and aggregate metrics by City (Delhi, Mumbai, Pune, Jaipur, Lucknow, All).
  - Listed restaurants count, order volume, gross food sales, and average customer rating.
- **7% Platform Fee Revenue**:
  - Platform commission strictly calculated as 7% of monthly sales from all restaurants across all cities.
- **Food Safety & Hygiene Grounds Verification**:
  - Mandatory audit workflow for newly enrolled kitchens (e.g., `Urban Wok Asian Kitchen`).
  - Genuine verification parameters:
    1. FSSAI License Number verification.
    2. Kitchen Cleanliness & Sanitation Audit Score (≥ 80 required).
    3. Water Potability & Safety Certification.
    4. Pest Control Audit Certification.
    5. Kitchen Fire Safety NOC.
  - Admin inspection evaluation with Approve / Reject action. Approved kitchens go live immediately.
- **Dual Database Health Widget**:
  - Live connectivity indicator for both `AssignmentDB` (Online) and `Practice` (Restored LMS database).

---

### 4. Delivery Partner Portal (`/DeliveryAgent`)
- **10 Active Delivery Agents**:
  - Pre-seeded active delivery agents across major cities with verified Aadhaar credentials.
  - Rider switcher dropdown for demonstration.
- **Online / Offline Duty Status**:
  - Toggle active duty status anytime.
- **Pickup & Drop Dashboard**:
  - Shows order pickup details (Restaurant Name, Address, City).
  - Delivery destination (Customer Name, Address, Distance in km).
  - ₹60 delivery fee earnings credited upon completion.
- **Simple Enrollment (`/DeliveryAgent/Enroll`)**:
  - 12-digit Aadhaar number with live verification check, vehicle type, vehicle registration number, and operating city.

---

### 5. Multi-Role Notification Hub
- Top navigation bar notification bell with unread badge counter and real-time dropdown alerts for Customers, Restaurants, Delivery Riders, and Admin.

---

## Running the Application
- Solution Path: `D:\Komal\C#\MVConFramework\MVConFramework.csproj`
- Built cleanly with MSBuild with 0 errors and 0 warnings.
- Runs on IIS Express or Visual Studio at: `http://localhost:55555/` or standard port.
- Default route is ready to navigate directly to `/Customer` or `/Account/Login`.
