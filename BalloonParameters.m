
close all
clear
clc
%% ========================================================================
% I planned to have 150 trials in each session of the experiment (50 with
% one agent, 50 with three agents, and 50 with all the six agents). In each
% trial I need 6 different numbers. So, overally I need 900 numbers. 450
% out of these 900 numbers are for risk takers, and 450 for conservatives!
RI1=rand(450,1);
NRI1=rand(450,1);
RI2=(1.7-1.5)*RI1+1.5;
NRI2=(2-1.8)*RI1+1.8;
RiskyInterval=(RI2.^6)*100;     % this gives 450 random numbers in the interval ~(1100,2400)
NonRiskyInterval=(NRI2.^6)*100; % this gives 450 random numbers in the interval ~(3400,6300)
% figure;plot(sort(RiskyInterval),'.');
% figure;plot(sort(NonRiskyInterval),'.');

RiskParam=mod(randperm(150),2); % if RiskParam==0, the experiment is NonRisky, and if it is 1, the experiment is Risky
%% making the parameters
finalStr="{{{";
% Shuffling agents' numbers. The last numer in each trial is zero because I
% wanted to keep the trial running after the last balloon bursts until the
% time for that trial is over!
for a1=1:150
    agentsStr=randperm(6);
    for a2=1:6
        finalStr=[finalStr,num2str(agentsStr(a2)),','];
    end
    finalStr=[finalStr,'0},{'];
end
finalStr=finalStr([1:end-1]);
finalStr=[finalStr,'0}},{{'];

% assigning the timings for bursting or quitting. I produce 6 numbers for
% each trial. So, for those trials that the number of agents is less than
% 6, some numbers are not used!
nR=0;                           % counter for risky trials
nNR=0;                          % counter for non-risky trials
for a1=1:150
%     agentsStr=randperm(6);
%     for a2=1:5
%         finalStr=[finalStr,num2str(agentsStr(a2)),','];
%     end
%     finalStr=[finalStr,'},{'];
    if (RiskParam(a1)==1)
        tempRiskyIntervals=sort(RiskyInterval(nR*6+1:(nR+1)*6))/1000;
        for a3=1:5
            finalStr=[finalStr,num2str(tempRiskyIntervals(a3)),','];
        end
        finalStr=[finalStr,num2str(tempRiskyIntervals(6)),',10},{'];
        nR=nR+1;
    else
        tempNonRiskyIntervals=sort(NonRiskyInterval(nNR*6+1:(nNR+1)*6))/1000;
        for a3=1:5
            finalStr=[finalStr,num2str(tempNonRiskyIntervals(a3)),','];
        end
        finalStr=[finalStr,num2str(tempNonRiskyIntervals(6)),',10},{'];
        nNR=nNR+1;
    end
end
finalStr=finalStr([1:end-1]);
finalStr=[finalStr,',10f}},{{'];
%% making quit or burst parameters
agentsParam=mod(randperm(150),3); % this parameter indicates how many agents 
% contribute in each trial. In 50 trials it is zero, meaning that only one 
% agent contributes in those trials. In 50 trials it is one, meaning that 3
% agents contribute in those trials. And finally in 50 trials it is 2,
% meaning that all the agents contribute in those trials.
for a1=1:150
    n1=1;   % counter for trials with one agent
    n3=1;   % counter for trials with three agents
    n6=1;   % counter for trials with six agent
    tempAgentsStr=zeros(7,1);
    QBParam=mod(randperm(900),2)+1; 
    if (agentsParam(a1)==0)
        agents=randperm(6,1);  % the only agent contributing in the current trial
        tempAgentsStr(agents)=QBParam(n1);
        n1=n1+1;
    elseif (agentsParam(a1)==1)
        agents=randperm(6,3);  % choose three agents who are contributing in the current trial
        tempAgentsStr(agents)=QBParam((n1-1)*6+agents);
        n3=n3+1;
    else
        agents=randperm(6,6);  % choose three agents who are contributing in the current trial
        tempAgentsStr(agents)=QBParam((n1-1)*6+agents);
        n6=n6+1;
    end
    for b1=1:6
        finalStr=[finalStr,num2str(tempAgentsStr(b1)),','];
    end
    finalStr=[finalStr,'0},{'];
end
finalStr=finalStr([1:end-1]);
finalStr=[finalStr,'0}}};'];


