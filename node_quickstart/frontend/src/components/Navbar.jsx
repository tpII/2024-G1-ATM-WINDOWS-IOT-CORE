import React from 'react'
import { Center, Container, Flex } from '@chakra-ui/react'

const Navbar = () => {
  return (
    <Container maxW={"1140px"} px={4}>
        <Flex
            h={16}
            alignItems={'center'}
            justifyContent={'space-between'}
            flexDir={{
                base:"column",
                sm:"row"
            }}
        >
            <Text 
                textStyle="md"
                textTransform={"uppercase"}
                textAlign={"center"}
                fontWeight="bold"
                bgGradient="to-r" gradientFrom="red.200" gradientTo="blue.200"
            >
                <Link to={"/"}>Product Store</Link>
                Sphinx of black quartz, judge my vow.
            </Text>
        </Flex>
    </Container>
  )
}

export default Navbar