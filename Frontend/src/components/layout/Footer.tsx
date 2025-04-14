import React from 'react'
import { useLocation } from 'react-router-dom'
import Container from './Container'
import { PrimaryButton, ProfileCard, Title } from '../common/UIComponents'

function Footer() {
  const location = useLocation()
  const isHomePage = location.pathname === '/'

  return (
    <footer className="relative bg-primary py-16 mt-16">
      {isHomePage && (
        <div className="bg-white w-full py-20 -mt-10 rounded-b-[40px] z-10 absolute top-0"></div>
      )}

      <Container className={`${isHomePage ? 'mt-32' : 'mt-0'} flex flex-col md:flex-row justify-between gap-12`}>
        <div className="w-full md:w-1/3">
          <img src="/images/logo-light.png" alt="Logo" />
          <br />
          <p className="text-gray-300">
            Supporting charitable causes through auctions with transparency and impact.
          </p>
          <div className="bg-gray-300 h-[1px] my-8"></div>
          <Title className="font-normal text-gray-100">Get The Latest Updates</Title>
          <div className="flex items-center justify-between mt-5">
            <input
              type="text"
              placeholder="Enter your email"
              className="w-full h-full p-3.5 py-[15px] text-sm border-none outline-none rounded-l-md"
            />
            <PrimaryButton className="rounded-none py-3.5 px-8 text-sm hover:bg-indigo-800 rounded-r-md">
              Submit
            </PrimaryButton>
          </div>
          <p className="text-gray-300 text-sm mt-3">Email is safe. We don't spam.</p>
        </div>
        <div className="grid grid-cols-2 md:grid-cols-4 gap-8 w-full md:w-2/3">
          <div>
            <Title level={5} className="text-white font-normal">
              Auction Categories
            </Title>
            <ul className="flex flex-col gap-5 mt-8 text-gray-200">
              <li>Ending Now</li>
              <li>Vehicles</li>
              <li>Watches</li>
              <li>Electronics</li>
              <li>Real Estate</li>
              <li>Jewelry</li>
              <li>Art</li>
              <li>Sports & Outdoor</li>
            </ul>
          </div>
          <div>
            <Title level={5} className="text-white font-normal">
              About Us
            </Title>
            <ul className="flex flex-col gap-5 mt-8 text-gray-200">
              <li>About Us</li>
              <li>Help</li>
              <li>Affiliates</li>
              <li>Jobs</li>
              <li>Press</li>
              <li>Our blog</li>
              <li>Collectors portal</li>
            </ul>
          </div>
          <div>
            <Title level={5} className="text-white font-normal">
              We are Here to Help
            </Title>
            <ul className="flex flex-col gap-5 mt-8 text-gray-200">
              <li>Your Account</li>
              <li>Safe and Secure</li>
              <li>Shipping Information</li>
              <li>Contact Us</li>
              <li>Help & FAQ</li>
            </ul>
          </div>
          <div>
            <Title level={5} className="text-white font-normal">
              Follow Us
            </Title>
            <ul className="flex flex-col gap-5 mt-8 text-gray-200">
              <li className="flex items-center gap-2">
                <span>📞</span>
                <span>(123) 456-7890</span>
              </li>
              <li className="flex items-center gap-2">
                <span>📧</span>
                <span>contact@charityauction.org</span>
              </li>
              <li className="flex items-center gap-2">
                <span>📍</span>
                <span>123 Charity Lane, City</span>
              </li>
            </ul>
            <div className="flex items-center mt-5 justify-between">
              <ProfileCard className="bg-white">
                YouTube
              </ProfileCard>
              <ProfileCard className="bg-white">
                Instagram
              </ProfileCard>
              <ProfileCard className="bg-white">
                Twitter
              </ProfileCard>
              <ProfileCard className="bg-white">
                LinkedIn
              </ProfileCard>
            </div>
          </div>
        </div>
      </Container>
    </footer>
  )
}

export default Footer
