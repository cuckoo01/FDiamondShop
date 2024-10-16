// bg-slate-400
// border-t-[1px] border-t-blue-950
//bg-[#061E47] (good)

import { Link } from 'react-router-dom';
import { FaInstagram } from "react-icons/fa";
import { RiFacebookFill } from "react-icons/ri";
import { FaTwitter } from "react-icons/fa";
import { IoLogoTiktok } from "react-icons/io5";
import { FaSnapchatGhost } from "react-icons/fa";

const Footer = () => {

    const noContentPath = '/no-content';

    return (
        <>
            <div className='font-gantari border-t-[1px] border-gray-400 h-auto px-16'>
                <div className='py-10'>

                    <ul className='flex flex-row justify-center items-start'>
                        <li className='flex flex-col capitalize mr-40 font-[350] space-y-3'>
                            <Link to={noContentPath}
                            >
                                <p className='font-semibold text-lg uppercase mb-1'>Customer Care</p>
                            </Link>

                            <Link to='tel:1800545457'
                                className='hover:text-gray-500 transition-colors duration-150'
                            >
                                <div className='flex space-x-2'>
                                    <img src='https://ecommo--ion.bluenile.com/bn-main/phone.447b6.svg' />
                                    <p>1800-54-54-57</p>
                                </div>
                            </Link>
                            <Link to='mailto:fdiamondshop391@gmail.com'
                                className='hover:text-gray-500 transition-colors duration-150'
                            >
                                <div className='flex space-x-2'>
                                    <img src='https://ecommo--ion.bluenile.com/bn-main/email.6041e.svg' />
                                    <p>Email Us</p>
                                </div>
                            </Link>
                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>Contact Us</Link>
                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>FAQ</Link>
                        </li>



                        <li className='flex flex-col capitalize mr-40 font-[350] space-y-3 '>
                            <Link to={noContentPath}
                            >
                                <p className='font-semibold text-lg uppercase mb-1'>About FDIAMOND</p>
                            </Link>

                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>Quality & Value</Link>
                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>Review</Link>
                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>Blog</Link>
                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>Diamond Sustainability</Link>
                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>Location</Link>
                        </li>



                        <li className='flex flex-col capitalize mr-40 font-[350] space-y-3 '>
                            <Link to={noContentPath}
                            >
                                <p className='font-semibold text-lg uppercase mb-1'>Legal Area</p>
                            </Link>

                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>terms of use</Link>
                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>privacy policy</Link>
                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>conditions of sale</Link>
                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>credits</Link>
                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>accessibility statement</Link>
                            <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'>Career</Link>
                        </li>


                        <li className='flex flex-col capitalize  font-[350] space-y-3 '>
                            <Link to={noContentPath}
                            >
                                <p className='font-semibold text-lg uppercase mb-1'>Follow Us</p>
                            </Link>

                            <div className='flex flex-row space-x-10'>
                                <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'><FaInstagram size={27} /></Link>
                                <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'><RiFacebookFill size={27} /></Link>
                                <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'><FaTwitter size={27} /></Link>
                                <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'><IoLogoTiktok size={27} /></Link>
                                <Link to={noContentPath} className='hover:text-gray-500 transition-colors duration-150'><FaSnapchatGhost size={27} /></Link>
                            </div>


                        </li>
                    </ul>

                </div>

                <div>
                    <p className='text-center text-[#888888] font-[350] text-sm py-5 border-t-[1px] border-gray-300'>© 2024 FDIAMOND. All Rights Reserved</p>
                </div>
            </div>
        </>
    )
};

export default Footer;
