import axios from 'axios';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

import Box from '@mui/material/Box';
import Link from '@mui/material/Link';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Divider from '@mui/material/Divider';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import LoadingButton from '@mui/lab/LoadingButton';
import { alpha, useTheme } from '@mui/material/styles';
import InputAdornment from '@mui/material/InputAdornment';

import { bgGradient } from 'src/theme/css';

import Iconify from 'src/components/iconify';

export default function LoginView() {
  const theme = useTheme();
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);

  const handleSubmit = async (event) => {
    event.preventDefault();
    setLoading(true);

    try {
      const response = await axios.post(
        'https://fdiamond-api.azurewebsites.net/api/Users/login',
        {
          userName,
          password,
        },
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );

      if (
        response.data &&
        response.data.isSuccess &&
        response.data.result &&
        response.data.result.token
      ) {
        const { token } = response.data.result;
        localStorage.setItem('token', token);
        navigate('/');
      } else {
        setError('Đăng nhập thất bại, vui lòng kiểm tra lại tên đăng nhập và mật khẩu.');
      }
    } catch (err) {
      console.error('Error logging in:', err.response ? err.response.data : err.message);
      if (err.response && err.response.data) {
        setError(
          err.response.data.errorMessages
            ? err.response.data.errorMessages.join(', ')
            : 'Đã có lỗi xảy ra, vui lòng thử lại sau.'
        );
      } else {
        setError('Đã có lỗi xảy ra, vui lòng thử lại sau.');
      }
    } finally {
      setLoading(false);
    }
  };

  const renderForm = (
    <>
      <Stack spacing={3}>
        <TextField
          label="User Name"
          fullWidth
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
          required
        />

        <TextField
          name="password"
          label="Password"
          type={showPassword ? 'text' : 'password'}
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          InputProps={{
            endAdornment: (
              <InputAdornment position="end">
                <IconButton onClick={() => setShowPassword(!showPassword)} edge="end">
                  <Iconify icon={showPassword ? 'eva:eye-fill' : 'eva:eye-off-fill'} />
                </IconButton>
              </InputAdornment>
            ),
          }}
        />
        {error && (
          <Typography color="error" gutterBottom>
            {error}
          </Typography>
        )}
      </Stack>

      <Stack direction="row" alignItems="center" justifyContent="flex-end" sx={{ my: 3 }}>
        <Link variant="subtitle2" underline="hover">
          Forgot password?
        </Link>
      </Stack>

      <LoadingButton
        fullWidth
        size="large"
        type="submit"
        variant="contained"
        color="inherit"
        onClick={handleSubmit}
        loading={loading}
      >
        Login
      </LoadingButton>
    </>
  );

  return (
    <Box
      sx={{
        ...bgGradient({
          color: alpha(theme.palette.background.default, 0.9),
          imgUrl: '/assets/background/overlay_4.jpg',
        }),
        height: 1,
      }}
    >
      <Stack alignItems="center" justifyContent="center" sx={{ height: 1 }}>
        <Card
          sx={{
            p: 5,
            width: 1,
            maxWidth: 420,
          }}
        >
          <Typography variant="h4">Sign in to FDiamond Admin</Typography>
          <Divider sx={{ my: 3 }} />
          {renderForm}
        </Card>
      </Stack>
    </Box>
  );
}
