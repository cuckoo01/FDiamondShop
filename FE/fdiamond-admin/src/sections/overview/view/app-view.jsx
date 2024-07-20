import axios from 'axios';
// import { faker } from '@faker-js/faker';
import { useLocation } from 'react-router';
import { useState, useEffect } from 'react';

import Container from '@mui/material/Container';
import Grid from '@mui/material/Unstable_Grid2';
import Typography from '@mui/material/Typography';
import {
  Alert,
  Select,
  Snackbar,
  MenuItem,
  AlertTitle,
  InputLabel,
  FormControl,
} from '@mui/material';

// import Iconify from 'src/components/iconify';

// import AppTasks from '../app-tasks';
// import AppNewsUpdate from '../app-news-update';
// import AppOrderTimeline from '../app-order-timeline';
import AppCurrentVisits from '../app-current-visits';
import AppWebsiteVisits from '../app-website-visits';
import AppWidgetSummary from '../app-widget-summary';

// import AppTrafficBySite from '../app-traffic-by-site';
// import AppCurrentSubject from '../app-current-subject';
// import AppConversionRates from '../app-conversion-rates';

// ----------------------------------------------------------------------

export default function AppView() {
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [purchasedOrders, setPurchasedOrders] = useState(0);
  const [completedOrders, setCompletedOrders] = useState(0);
  const [soldProducts, setSoldProducts] = useState(0);
  const [totalPriceCompletedOrders, setTotalPriceCompletedOrders] = useState(0);
  const [beforeDiscount, setBeforeDiscount] = useState(0);
  const [selectedYear, setSelectedYear] = useState(new Date().getFullYear());

  const location = useLocation();

  useEffect(() => {
    if (location.state && location.state.showSnackbar) {
      setOpenSnackbar(true);
      // Clear the state to avoid showing Snackbar again if the user navigates back to this page
      window.history.replaceState({}, document.title);
    }
  }, [location]);

  useEffect(() => {
    axios
      .get('https://fdiamond-api.azurewebsites.net/api/Order/GetAllOrder')
      .then((response) => {
        if (response.data.isSuccess) {
          const ordersByYear = response.data.result.filter(
            (order) => new Date(order.orderDate).getFullYear() === selectedYear
          );

          const purchaseOrdersData = ordersByYear.filter(
            (purchaseOrder) => purchaseOrder.status === 'Ordered'
          );

          const purchaseOrdersCount = purchaseOrdersData.length || 0;
          setPurchasedOrders(purchaseOrdersCount);

          const completedOrdersData = ordersByYear.filter((order) => order.status === 'Completed');

          const completedOrdersCount = completedOrdersData.length || 0;
          setCompletedOrders(completedOrdersCount);

          const soldProductsCount =
            completedOrdersData.reduce((total, order) => {
              const orderQuantity = order.cartLines.reduce((orderTotal, cartLine) => {
                const cartLineQuantity = cartLine.cartLineItems.reduce(
                  (cartLineTotal, item) => cartLineTotal + 1, // Assuming each item in cartLineItems represents one product
                  0
                );
                return orderTotal + cartLineQuantity;
              }, 0);
              return total + orderQuantity;
            }, 0) || 0;

          setSoldProducts(soldProductsCount);

          const totalPrice =
            completedOrdersData.reduce((total, order) => total + order.totalPrice, 0) || 0;

          setTotalPriceCompletedOrders(totalPrice);

          const totalBeforeDiscount =
            completedOrdersData.reduce((total, order) => total + order.basePrice, 0) || 0;

          setBeforeDiscount(totalBeforeDiscount);
          console.log(totalBeforeDiscount, soldProductsCount, completedOrdersCount);
        } else {
          console.error('Error in response data:', response.data.errorMessages);
        }
      })
      .catch((error) => {
        console.error('Error fetching orders:', error);
      });
  }, [selectedYear]);

  const handleYearChange = (event) => {
    setSelectedYear(event.target.value);
  };

  const Average = completedOrders > 0 ? totalPriceCompletedOrders / completedOrders : 0;
  const Discount = beforeDiscount - totalPriceCompletedOrders;

  const handleCloseSnackbar = () => {
    setOpenSnackbar(false);
  };
  return (
    <Container maxWidth="xl">
      <Snackbar
        open={openSnackbar}
        autoHideDuration={6000}
        onClose={handleCloseSnackbar}
        anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
      >
        <Alert onClose={handleCloseSnackbar} severity="success" sx={{ width: '100%' }}>
          <AlertTitle>Success</AlertTitle>
          Update Account Profile Successfully
        </Alert>
      </Snackbar>
      <Typography variant="h4" sx={{ mb: 5 }}>
        Hi, Welcome back 👋
      </Typography>

      <FormControl sx={{ mb: 2, minWidth: 200 }}>
        <InputLabel id="select-year-label">Year</InputLabel>
        <Select
          labelId="select-year-label"
          id="select-year"
          value={selectedYear}
          onChange={handleYearChange}
          label="Year"
        >
          {Array.from({ length: 10 }, (_, i) => (
            <MenuItem key={i} value={new Date().getFullYear() - i}>
              {new Date().getFullYear() - i}
            </MenuItem>
          ))}
        </Select>
      </FormControl>

      <Grid container spacing={3}>
        <Grid xs={12} sm={6} md={3}>
          <AppWidgetSummary
            title="Purchase Order"
            total={purchasedOrders}
            color="warning"
            sx={{ backgroundColor: '#1877F2', color: '#FFFFFF' }}
            // icon={<img alt="icon" src="/assets/icons/glass/ic_glass_buy.png" />}
          />
        </Grid>

        <Grid xs={12} sm={6} md={3}>
          <AppWidgetSummary
            title="Completed Order (1)"
            total={completedOrders}
            color="success"
            sx={{ backgroundColor: '#00A76F', color: '#FFFFFF' }}
            // icon={<img alt="icon" src="/assets/icons/glass/ic_glass_bag.png" />}
          />
        </Grid>

        <Grid xs={12} sm={6} md={3}>
          <AppWidgetSummary
            title="Product Sold"
            total={soldProducts}
            color="info"
            sx={{ backgroundColor: '#FFAB00', color: '#000000' }}
            // icon={<img alt="icon" src="/assets/icons/glass/ic_glass_users.png" />}
          />
        </Grid>

        <Grid xs={12} sm={6} md={3}>
          <AppWidgetSummary
            title="Average invoice (5)=(4)/(1) "
            type="currency"
            total={Average}
            color="error"
            sx={{ backgroundColor: '#8E33FF', color: '#FFFFFF' }}
          />
        </Grid>

        <Grid xs={12} sm={6} md={3}>
          <Grid xs={12} sm={6} md={3} sx={{ mb: 3 }}>
            <AppWidgetSummary
              title="Before Discount (2)"
              type="currency"
              total={beforeDiscount}
              color="error"
            />
          </Grid>

          <Grid xs={12} sm={6} md={3} sx={{ mb: 3 }}>
            <AppWidgetSummary title="Discount (3)" type="currency" total={Discount} color="error" />
          </Grid>

          <Grid xs={12} sm={6} md={3}>
            <AppWidgetSummary
              title="Actual Revenue (4)=(2)-(3)"
              type="currency"
              total={totalPriceCompletedOrders}
              color="error"
              // icon={<img alt="icon" src="/assets/icons/glass/ic_glass_message.png" />}
            />
          </Grid>
        </Grid>

        <Grid xs={12} md={6} lg={8}>
          <AppWebsiteVisits
            title="Website Visits"
            subheader="(+43%) than last year"
            chart={{
              labels: [
                '01/01/2003',
                '02/01/2003',
                '03/01/2003',
                '04/01/2003',
                '05/01/2003',
                '06/01/2003',
                '07/01/2003',
                '08/01/2003',
                '09/01/2003',
                '10/01/2003',
                '11/01/2003',
              ],
              series: [
                // {
                //   name: 'Team A',
                //   type: 'column',
                //   fill: 'solid',
                //   data: [23, 11, 22, 27, 13, 22, 37, 21, 44, 22, 30],
                // },
                {
                  name: 'Team B',
                  type: 'area',
                  fill: 'gradient',
                  data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43],
                },
                {
                  name: 'Team C',
                  type: 'line',
                  fill: 'solid',
                  data: [30, 25, 36, 30, 45, 35, 64, 52, 59, 36, 39],
                },
              ],
            }}
          />
        </Grid>

        <Grid xs={12} md={6} lg={4}>
          <AppCurrentVisits
            title="Current Visits"
            chart={{
              series: [
                { label: 'America', value: 4344 },
                { label: 'Asia', value: 5435 },
                { label: 'Europe', value: 1443 },
                { label: 'Africa', value: 4443 },
              ],
            }}
          />
        </Grid>
      </Grid>
    </Container>
  );
}
