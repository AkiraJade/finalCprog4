import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class pAdvLevel4 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class pAdvLevel4 extends World
{

    /**
     * Constructor for objects of class pAdvLevel4.
     * 
     */
     enum PlatformState {
        WAITING, MOVING, STOPPED, REMOVED, DISAPPEAR, SPAWNED, APPEAR
    }

    enum SpikeState {
        NOT_CREATED, CREATED, READY, MOVING, STOPPED, DISAPPEAR, APPEAR
    }

    enum ShurikenState {
        NOT_CREATED, CREATED, READY, MOVING, STOPPED, DISAPPEAR, APPEAR
    }
    
    int playerX;
    int playerY;
    Shuriken s1;
    Shuriken s2;
    Shuriken s3;
    Shuriken s4;
    Platform p11;
    Spike sp1;
    Spike sp2;
    ShurikenState s1State = ShurikenState.CREATED;
    ShurikenState s2State = ShurikenState.CREATED;
    ShurikenState s3State = ShurikenState.CREATED;
    ShurikenState s4State = ShurikenState.CREATED;
    SpikeState sp1State = SpikeState.CREATED;
    PlatformState p11State = PlatformState.WAITING;
      SpikeState sp2State = SpikeState.CREATED;
    public pAdvLevel4()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(750, 400, 1);
        prepare();
    }
    
    public void prepare(){
      
        Platform p1 = new Platform(100, 250);
        addObject(p1, 30, 275);
        Platform p2 = new Platform(700, 20);
        addObject(p2, 400, 390);
        s3 = new Shuriken();
        addObject(s3, 197, 286);
        Platform p3 = new Platform(100, 520);
        addObject(p3, 200, 70);
        s1 = new Shuriken();
        addObject(s1, 78, 195);
        s2 = new Shuriken();
        addObject(s2, 147, 255);
        sp1 = new Spike();
        GreenfootImage sp1Image = sp1.getImage();
        sp1Image.rotate(180);
        s4 = new Shuriken();
        addObject(s4, 375, 360);
        Platform p4 = new Platform(500, 150);
        addObject(p4, 600, 310);
         
        Platform p5 = new Platform(50, 10);
        addObject(p5, 330, 330);
        Platform p8 = new Platform(250, 300);
        addObject(p8, 630, 100);
        Spike sp3 = new Spike();
        addObject(sp3, 454, 227);
        Spike sp4 = new Spike();
        addObject(sp4, 485, 227);
        Spike sp5 = new Spike();
        addObject(sp5, 430, 227);
        Platform p9 = new Platform(20, 10);
        addObject(p9, 420, 210);
        Platform p10 = new Platform(50, 10);
        addObject(p10, 500, 180);
        p11 = new Platform(40, 190);
        addObject(p11, 530, 80);
             Platform p15 = new Platform(90, 10);
        addObject(p15, 270, 160);  
        FakeAdvPortal fp1 = new FakeAdvPortal();
        addObject(fp1,280, 120);
          Person player1 = new Person();
        addObject(player1, 30, 131);
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
        //266
        //197 286
    }
    
    public void act(){
        updatePlayerPosition();
        trapsV1();
        trapsV2();
    }
     public void updatePlayerPosition(){
        java.util.List<Person> players = getObjects(Person.class);
        
        if(players.isEmpty()){
            return;
        }
        
        Person player = players.get(0);
        playerX = player.getX();
        playerY = player.getY();
    }
    
    boolean secondMove = false;
    public void trapsV1(){
        
        if(playerX >= 100 && playerY <= 102
        && s1State == ShurikenState.CREATED){
            s1State = ShurikenState.MOVING;
        }
        
        if (s1State == ShurikenState.MOVING){
            s1.setLocation(s1.getX() + 3, s1.getY());
            
            if(s1.getX() >= 150){
                s1State = ShurikenState.STOPPED;
            }
        }
        
        if(s1State == ShurikenState.STOPPED && s2State == ShurikenState.CREATED &&
        playerY <= 195){
              s2State = ShurikenState.MOVING;
        }
        
         if (s2State == ShurikenState.MOVING){
            s2.setLocation(s2.getX() -3, s2.getY());
            
            if(s2.getX() <= 78){
                s2State = ShurikenState.STOPPED;
            }
        }
        
        if(playerX >= 110 && playerY <= 290 && s3State == ShurikenState.CREATED){
            s3State = ShurikenState.MOVING;
        }
        
        if(s3State == ShurikenState.MOVING && !secondMove){
            s3.setLocation(s3.getX() -5, s3.getY());
            
            if(s3.getX() <= 150){
                s3State = ShurikenState.STOPPED;
                secondMove = true;
            }
        }
        
        if(playerX >= 138 && playerY >= 360 && sp1State == SpikeState.CREATED){
             addObject(sp1, 170, 337);
            sp1State = SpikeState.APPEAR;
        }
        
        if(sp1State == SpikeState.APPEAR && playerX <= 118){
           removeObject(sp1);
           sp1 = null;
        }
        
        if(sp1 == null && playerX >= 266 && s4State == ShurikenState.CREATED){
            s4State = ShurikenState.MOVING;
            
        }
         
        if(s4State == ShurikenState.MOVING){
            s4.setLocation(s4.getX() - 10, s4.getY());
            
            if(s4.getX() <= 78){
                s4State = ShurikenState.STOPPED;
                 Platform p6 = new Platform(40, 10);
        addObject(p6, 270, 290);
            }
        }
        
        
        
    
        //288 266
    }
    boolean obsAppear = false;
    boolean goesBack = false;
    Spike  sp7;
    boolean portalReady = false;
    public void trapsV2(){
        if(playerX <= 288 && playerY <= 266 && s3State == ShurikenState.STOPPED){
            s3State = ShurikenState.MOVING;
        }
        
        if (s3State == ShurikenState.MOVING && secondMove){
            s3.setLocation(s3.getX() +5, s3.getY());
            
            if(s3.getX() >= 350){
                s3State = ShurikenState.DISAPPEAR;
              Platform p7 = new Platform(50, 10);
        addObject(p7, 330, 240);
            }
        } //340
        
        if(playerX >= 340 && sp2State == SpikeState.CREATED){
             sp2 = new Spike();
        addObject(sp2, 365, 227);
        sp2State = SpikeState.APPEAR;
        }
        
        if(sp2State == SpikeState.APPEAR && playerX <= 326){
            removeObject(sp2);
        }
        
        
        if(playerX >= 442 && playerY <= 126 && p11State == PlatformState.WAITING){
            p11State = PlatformState.MOVING;
        }
        
        if(p11State == PlatformState.MOVING && !goesBack){
            p11.setLocation(p11.getX() - 5, p11.getY());
            
            if(p11.getX() <= 495){
                p11State = PlatformState.STOPPED;
            }
            
        }
        
        if(playerX <= 433 && p11State == PlatformState.STOPPED){
            goesBack = true;
        }
        
        if(goesBack){
            p11.setLocation(p11.getX() + 10, p11.getY());
            
            if(p11.getX() >= 600){
                p11State = PlatformState.DISAPPEAR;
            }
        }
        if (playerX >= 484 && playerY >= 155 && !obsAppear){
            Platform p12 = new Platform(50, 10);
        addObject(p12, 370, 160);
        Platform p13 = new Platform(20, 10);
        addObject(p13,450, 120);
        Platform p14 = new Platform(50, 10);
        addObject(p14, 370, 80);
        
        sp7 = new Spike();
        addObject(sp7, 370,147);
        obsAppear = true;
        //495 442 126
        
        
        }
        
        if(playerX <= 422 && playerY <= 35 && playerX >= 286){
            Spike sp6 = new Spike();
        addObject(sp6, 370, 67);
        }
        
        if(playerX >=  408 && playerX <= 430 && playerY >= 185 ){
           removeObject(sp7);
           portalReady = true;
        }
        
        if(playerX <= 283 && playerY == 136 && portalReady){
            advPortal ap1 = new advPortal(5);
        addObject(ap1, 120, 345);
        }
        
    }
}