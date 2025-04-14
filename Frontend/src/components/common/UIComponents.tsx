import React from 'react'
import { Link } from 'react-router-dom'

interface CustomNavLinkProps {
  href: string
  children: React.ReactNode
  className?: string
}

export function CustomNavLink({ href, children, className = '' }: CustomNavLinkProps) {
  return (
    <Link to={href} className={`transition duration-300 hover:opacity-75 ${className}`}>
      {children}
    </Link>
  )
}

interface CustomNavLinkListProps {
  href: string
  children: React.ReactNode
  className?: string
  isActive?: boolean
}

export function CustomNavLinkList({ href, children, className = '', isActive = false }: CustomNavLinkListProps) {
  return (
    <Link 
      to={href} 
      className={`transition duration-300 hover:opacity-75 ${isActive ? 'font-bold' : ''} ${className}`}
    >
      {children}
    </Link>
  )
}

interface ProfileCardProps {
  children: React.ReactNode
  className?: string
}

export function ProfileCard({ children, className = '' }: ProfileCardProps) {
  return (
    <div className={`w-10 h-10 rounded-full flex items-center justify-center overflow-hidden ${className}`}>
      {children}
    </div>
  )
}

interface TitleProps {
  children: React.ReactNode
  className?: string
  level?: 1 | 2 | 3 | 4 | 5 | 6
}

export function Title({ children, className = '', level = 1 }: TitleProps) {
  const Tag = `h${level}` as React.ElementType
  
  return (
    <Tag className={`font-bold ${className}`}>
      {children}
    </Tag>
  )
}

interface ButtonProps {
  children: React.ReactNode
  className?: string
  onClick?: () => void
  type?: 'button' | 'submit' | 'reset'
}

export function PrimaryButton({ 
  children, 
  className = '', 
  onClick,
  type = 'button'
}: ButtonProps) {
  return (
    <button
      type={type}
      onClick={onClick}
      className={`bg-green text-white font-medium py-2 px-4 rounded-md transition duration-300 hover:opacity-90 ${className}`}
    >
      {children}
    </button>
  )
} 