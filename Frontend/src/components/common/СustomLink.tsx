import { forwardRef } from 'react';
import { Link, LinkProps } from 'react-router-dom';

// Це зробить тип сумісним з React-Bootstrap "as"
const CustomLink = forwardRef<HTMLAnchorElement, LinkProps>((props, ref) => {
  return <Link ref={ref} {...props} />;
});

export default CustomLink;
